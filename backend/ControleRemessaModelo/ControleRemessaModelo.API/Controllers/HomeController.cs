using ControleRemessaModelo.API.Excecoes;
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
            DefaultResponseAPI responseAPI = new();

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
                Console.WriteLine(ex.ToString());

                ErrorMessageResponseAPI erro = new() { ErrorCode = 500, ErrorMessage = "Erro desconhecido." };
                responseAPI.Errors.Add(erro);

                return Ok(responseAPI);
            }
        }
    }
}
