using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.LogJogoDto;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogAlteracaoJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogAlteracaoJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerLogJogoDto>> Listar()
        {
            try
            {
                var logs = _service.Listar();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao listar logs", erro = ex.Message });
            }
        }

        [HttpGet("jogo/{jogoId}")]
        public ActionResult<List<LerLogJogoDto>> ListarPorJogo(int jogoId)
        {
            try
            {
                var logs = _service.ListarPorJogo(jogoId);

                if (logs == null || logs.Count == 0)
                {
                    return NotFound(new { mensagem = "Nenhum log encontrado para este jogo." });
                }

                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao buscar logs do jogo", erro = ex.Message });
            }
        }
    }
}