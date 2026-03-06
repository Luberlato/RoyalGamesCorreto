using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo? ObterPorId(int id);
        List<Jogo>? ObterPorNome(string nome);
        public byte[] ObterImagemPorId(int id);
        void Adicionar(Jogo jogo, List<int> GeneroIds);
        void Atualizar(Jogo jogo, List<int> GeneroIds);
        void Remover(int id);
    }
}
