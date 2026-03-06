using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.Dtos.JogoDto;
using RoyalGames.Excpetions;
using System.Security.Claims;

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

        private int ObterUsuarioId()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idTexto) || !int.TryParse(idTexto, out int id))
            {
                throw new DomainException("Usuário não autenticado.");
            }

            return int.Parse(idTexto);
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

        [HttpGet("{id}")]
        public ActionResult ObterPorId(int id)
        {
            try
            {
                LerJogoDto jogo = _service.ObterPorId(id);
                return Ok(jogo);
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        [HttpGet("/nome/{nome}")]
        public ActionResult ObterPorNome(string nome)
        {
            try
            {
                List<LerJogoDto> jogos = _service.ObterPorNome(nome);
                return Ok(jogos);
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        [HttpGet("imagem/{id}")]
        public ActionResult ObterImagemPorId(int id)
        {
            try
            {
                var imagem = _service.ObterImagemPorId(id);
                return Ok(imagem);
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Adicionar(CriarJogoDto jogoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioId();
                LerJogoDto jogo = _service.Adicionar(jogoDto, usuarioId);
                return Ok(jogoDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(id, jogoDto);
                return Ok(jogoDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Deletar(int id)
        {
            try
            {
                _service.remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
