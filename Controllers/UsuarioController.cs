using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.Dtos.UsuarioDtos;
using RoyalGames.Excpetions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Listar()
        {
            try
            {
                List<LerUsuarioDto> produtos = _service.Listar();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult ObterPorID(int id)
        {
            try
            {
                LerUsuarioDto usuario = _service.ObterPorId(id);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult CadastrarUsuario(CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuario =  _service.CadastrarUsuario(usuarioDto);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            try
            {
                _service.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
