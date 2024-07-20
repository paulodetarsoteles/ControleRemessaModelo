using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Negocio.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ControleRemessaModelo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [SwaggerOperation(Summary = "Lista todos os usuários cadastrados")]
        [HttpGet("listar")]
        [Authorize(Roles = "admin")]
        public IActionResult Listar()
        {
            DefaultResponseAPI resultado = _usuarioServico.Buscar();

            return Ok(resultado);
        }
    }
}
