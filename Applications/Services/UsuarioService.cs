using RoyalGames.Domains;
using RoyalGames.Dtos.UsuarioDtos;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerDto = new LerUsuarioDto
            {
                Id = usuario.UsuarioID,
                Email = usuario.Email,
                Nome = usuario.Nome,
                StatusUsuario = usuario.StatusUsuario ?? true
            };
            return lerDto;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();

            return usuariosDto;

        }

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            return LerDto(usuario);
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) 
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
        public LerUsuarioDto CadastrarUsuario(CriarUsuarioDto criarUsuario)
        {
            Usuario usuario = new Usuario
            {
                Nome = criarUsuario.Nome,
                Email = criarUsuario.Email,
                Senha = HashSenha(criarUsuario.senha),

            };

            _repository.CadastrarUsuario(usuario);

            return LerDto(usuario);
        }

        public void Deletar(int id)
        {
            _repository.Deletar(id);
        }
        
    }

}
