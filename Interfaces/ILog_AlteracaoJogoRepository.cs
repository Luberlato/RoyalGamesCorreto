using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface ILog_AlteracaoJogoRepository
    {
        void SalvarLog(Log_AlteracaoJogo log);
        List<Log_AlteracaoJogo> Listar();
        List<Log_AlteracaoJogo> ListarPorJogo(int jogoId);
    }
}