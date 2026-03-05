using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RoyalGames.Repositories
{
    public class Log_AlteracaoJogoRepository : ILog_AlteracaoJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public Log_AlteracaoJogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public void SalvarLog(Log_AlteracaoJogo log)
        {
            _context.Log_AlteracaoJogo.Add(log);
            _context.SaveChanges();
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            return _context.Log_AlteracaoJogo
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();
        }

        public List<Log_AlteracaoJogo> ListarPorJogo(int jogoId)
        {
            return _context.Log_AlteracaoJogo
                .Where(l => l.JogoID == jogoId)
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();
        }
    }
}