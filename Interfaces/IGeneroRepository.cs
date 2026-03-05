using RoyalGames.Domains;
using RoyalGames.Dtos.UsuarioDtos;
using RoyalGames.Repositories;

namespace RoyalGames.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> Listar();
        Genero? ObterPorId(int id);
        void Adicionar(Genero genero);
        void Atualizar(Genero genero);
        void Deletar(int id);
        bool NomeExiste(string nome, int? id = null);
    }
}
