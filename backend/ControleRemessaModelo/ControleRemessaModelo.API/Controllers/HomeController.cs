using ControleRemessaModelo.API.Responses.Home;
using ControleRemessaModelo.API.Services;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Negocio.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ControleRemessaModelo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly AutenticacaoUsuarioJWT _tokenServ;
        private readonly IUsuarioServico _usuarioServico;

        public HomeController(AutenticacaoUsuarioJWT tokenServ, IUsuarioServico usuarioServico)
        {
            _tokenServ = tokenServ;
            _usuarioServico = usuarioServico;
        }

        [SwaggerOperation(Summary = "Verifica se a API está funcionando")]
        [HttpGet("index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            IndexResponse response = new();
            DefaultResponseAPI responseAPI = new(null, [response], true, 200);

            return Ok(responseAPI);
        }

        [SwaggerOperation(Summary = "Verifica aenas se o usuário admin está sendo validado")]
        [HttpGet("health")]
        [Authorize(Roles = "admin")]
        public IActionResult Health()
        {
            HealthResponse response = new() { Authorization = true, HealthCheck = true };
            DefaultResponseAPI responseAPI = new(null, [response], true, 200);

            return Ok(responseAPI);
        }

        [SwaggerOperation(Summary = "Faz o login no sistema (retorna o token)")]
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UsuarioLoginDTO login)
        {
            login.Senha = _tokenServ.GetHashMd5(login.Senha);
            DefaultResponseAPI responseAPI = _tokenServ.GenerateToken(login);

            return Ok(responseAPI);
        }
    }
}
