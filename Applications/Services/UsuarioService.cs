using RoyalGames.Applications.Regras;
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
            return new LerUsuarioDto
            {
                Id = usuario.UsuarioID,
                Email = usuario.Email,
                Nome = usuario.Nome,
                StatusUsuario = usuario.StatusUsuario ?? true
            };
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            return usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email inválido.");
            }
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

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);
            if (usuario == null) throw new DomainException("Usuario não encontrado");
            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);
            if (usuario == null) throw new DomainException("Usuário não existe.");
            return LerDto(usuario);
        }

        public LerUsuarioDto CadastrarUsuario(CriarUsuarioDto criarUsuario)
        {
            Validacoes.ValidarUsuario(criarUsuario);

            if (_repository.EmailExiste(criarUsuario.Email))
            {
                throw new DomainException("Já existe um usuário com este e-mail");
            }

            Usuario usuario = new Usuario
            {
                Nome = criarUsuario.Nome,
                Email = criarUsuario.Email,
                Senha = HashSenha(criarUsuario.Senha),
                StatusUsuario = true
            };

            _repository.CadastrarUsuario(usuario);
            return LerDto(usuario);
        }

        public LerUsuarioDto Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            Usuario? usuarioBanco = _repository.ObterPorId(id);
            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado.");

            ValidarEmail(usuarioDto.Email);

            Usuario? usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);
            if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != id)
            {
                throw new DomainException("Já existe um usuário com este e-mail.");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);
            return LerDto(usuarioBanco);
        }

        public void Deletar(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);
            if (usuario == null) throw new DomainException("Usuário não encontrado.");
            _repository.Deletar(id);
        }

    }
}