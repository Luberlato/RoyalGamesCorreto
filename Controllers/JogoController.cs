using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.Dtos.JogoDto;
using RoyalGames.Excpetions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerJogoDto> jogos = _service.Listar();
                return Ok(jogos);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
