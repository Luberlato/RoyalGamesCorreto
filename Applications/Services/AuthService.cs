using RoyalGames.Applications.Autenticacao;
using RoyalGames.Domains;
using RoyalGames.Dtos.Auth;
using RoyalGames.Dtos.AuthDto;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AuthService(IUsuarioRepository repository, GeradorTokenJwt tokenJwt)
        {
            _repository = repository;
            _tokenJwt = tokenJwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);

            if (usuario == null)
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { Token = token };

            return novoToken;
        }
    }
}
