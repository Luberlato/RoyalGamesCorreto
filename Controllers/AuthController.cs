using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Autenticacao;
using RoyalGames.Applications.Services;
using RoyalGames.Domains;
using RoyalGames.Dtos.Auth;
using RoyalGames.Excpetions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                var token = _service.Login(loginDto);

                return Ok(token);
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }



        }
    }
}
