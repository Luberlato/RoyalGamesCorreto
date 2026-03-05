using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RoyalGamesContext _context;

        public UsuarioRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(usuario => usuario.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(usuario => usuario.Email == email);
        }

        public void Deletar(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioID == id);

            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(Auxiliar => Auxiliar.UsuarioID == usuario.UsuarioID);

            if (usuarioBanco != null)
            {
                usuarioBanco.Nome = usuario.Nome;
                usuarioBanco.Email = usuario.Email;
                usuarioBanco.Senha = usuario.Senha;

                _context.SaveChanges();
            }
        }
    }
}