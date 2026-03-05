using RoyalGames.Domains;
using RoyalGames.Dtos.UsuarioDtos;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
<<<<<<< HEAD
        List<Usuario> Listar();
        void CadastrarUsuario(Usuario usuario);
        Usuario? ObterPorId(int id);

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Deletar(int id);
        void Atualizar(Usuario usuario);
=======
         List<Usuario> Listar();
         void CadastrarUsuario(Usuario usuario);
         Usuario? ObterPorId(int id);
         Usuario? ObterPorEmail(string email);
         void Deletar(int id);
         void Atualizar(Usuario usuario);
>>>>>>> 7d142ff32cc115f10afea464e52171359e923c14
    }
}
