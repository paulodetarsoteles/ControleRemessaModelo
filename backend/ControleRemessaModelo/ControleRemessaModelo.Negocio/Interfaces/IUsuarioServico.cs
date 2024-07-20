using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Responses;

namespace ControleRemessaModelo.Negocio.Interfaces
{
    public interface IUsuarioServico
    {
        UsuarioDTO? GetUsuarioLogin(string userName);
        DefaultResponseAPI Buscar();
    }
}
