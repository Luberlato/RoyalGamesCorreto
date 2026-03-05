using RoyalGames.Domains;
using RoyalGames.Dtos.UsuarioDtos;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        void CadastrarUsuario(Usuario usuario);
        Usuario? ObterPorId(int id);

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Deletar(int id);
        void Atualizar(Usuario usuario);
    }
}
