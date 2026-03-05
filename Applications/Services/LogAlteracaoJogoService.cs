using RoyalGames.Domains;
using RoyalGames.DTOs.LogJogoDto; // Certifique-se de criar este namespace/pasta
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILog_AlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILog_AlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            return logs.Select(log => new LerLogJogoDto
            {
                AlteracaoID = log.AlteracaoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();
        }

        public List<LerLogJogoDto> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(jogoId);

            return logs.Select(log => new LerLogJogoDto
            {
                AlteracaoID = log.AlteracaoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();
        }
    }
}