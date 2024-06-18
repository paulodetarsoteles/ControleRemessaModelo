using ControleRemessaModelo.API.Services;
using ControleRemessaModelo.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleRemessaModelo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Api funcionando.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginViewModel login)
        {
            if (login.UserName != "usuario" && login.Password != "senha")
                return Unauthorized();

            string token = AutenticacaoUsuarioJWT.GenerateJwtToken(login.UserName);

            return Ok(new { token });
        }
    }
}
