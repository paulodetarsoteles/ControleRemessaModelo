using ControleRemessaModelo.API.Responses;
using ControleRemessaModelo.API.Services;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleRemessaModelo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IAutenticacaoUsuarioJWT _tokenServ;
        private readonly IUsuarioServico _usuarioServico;

        public HomeController(IAutenticacaoUsuarioJWT tokenServ, IUsuarioServico usuarioServico)
        {
            _tokenServ = tokenServ;
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            //TODO: Implementar para salvar as requisicoes no MongoDB
            DefaultResponseAPI responseAPI = new(null, [new { message = "API funcionando!" }], true, 200);

            return Ok(responseAPI);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO login)
        {
            //TODO: Implementar para salvar as requisicoes no MongoDB
            DefaultResponseAPI responseAPI = new();

            try
            {
                //Encapsular para um métdo de serviço de autenticação e validar o usuario e o password

                string token = _tokenServ.GenerateToken(login).ToString() ??
                    string.Empty;

                if (string.IsNullOrEmpty(token))
                {
                    ErrorMessageResponseAPI erro = new()
                    {
                        ErrorCode = 204,
                        ErrorMessage = ErrorMessages.None
                    };

                    responseAPI = new([erro], [], false, 204);

                    return Ok(responseAPI);
                }

                responseAPI = new(null, [new { token }], true, 200);

                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                ErrorMessageResponseAPI erro = new()
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                };

                responseAPI.Errors.Add(erro);

                return Ok(responseAPI);
            }
        }
    }
}
