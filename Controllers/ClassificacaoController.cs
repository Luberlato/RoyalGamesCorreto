using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.Dtos.ClassificacaoDtos;
using RoyalGames.Excpetions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificacaoDto>> Listar()
        {
            var lista = _service.Listar();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<LerClassificacaoDto> ObterPorId(int id)
        {
            try
            {
                var classificacao = _service.ObterPorId(id);
                return Ok(classificacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarClassificacaoDto criarDto)
        {
            try
            {
                _service.Cadastrar(criarDto);

                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, CriarClassificacaoDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Deletar(int id)
        {
            try
            {
                _service.Deletar(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}