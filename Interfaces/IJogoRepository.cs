using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo? ObterPorId(int id);
        Jogo? ObterPorNome(string nome);
        void Adicionar(Jogo jogo, List<int> CatgeoriasIds);
        void Atualizar(Jogo jogo, List<int> categoriaIds);
        void Remover(int id);
    }
}
