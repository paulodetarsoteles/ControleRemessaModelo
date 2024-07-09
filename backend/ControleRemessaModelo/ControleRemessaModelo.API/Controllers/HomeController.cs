using ControleRemessaModelo.API.Excecoes;
using ControleRemessaModelo.API.Responses;
using ControleRemessaModelo.API.Responses.Home;
using ControleRemessaModelo.API.Services;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Reflection;

namespace ControleRemessaModelo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAutenticacaoUsuarioJWT _tokenServ;
        private readonly IUsuarioServico _usuarioServico;

        public HomeController(ILogger<HomeController> logger, IAutenticacaoUsuarioJWT tokenServ, IUsuarioServico usuarioServico)
        {
            _logger = logger;
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
            DefaultResponseAPI responseAPI = new();
            login.Senha = _tokenServ.GetHashMd5(login.Senha);

            try
            {
                string token = _tokenServ.GenerateToken(login).ToString();
                responseAPI = new(null, [new { token }], true, 200);

                return Ok(responseAPI);
            }
            catch (CustomException ex)
            {
                ErrorMessageResponseAPI erro = new() { ErrorCode = 500, ErrorMessage = ex.Message };
                erro.ErrorCode = (int)ex.ErrorCode;
                erro.ErrorMessage = ErrorMessages.GetMessage(ex.ErrorCode);
                responseAPI.Errors.Add(erro);
                responseAPI.StatusCode = ErrorMessages.RetornarStatucCode(ex.ErrorCode);

                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{TypeDescriptor.GetClassName(this)} - {nameof(MethodBase)} - {ex.Message} - {ex.StackTrace}", "ERRO");

                ErrorMessageResponseAPI erro = new() { ErrorCode = 500, ErrorMessage = "Erro desconhecido." };
                responseAPI.Errors.Add(erro);

                return Ok(responseAPI);
            }
        }
    }
}
