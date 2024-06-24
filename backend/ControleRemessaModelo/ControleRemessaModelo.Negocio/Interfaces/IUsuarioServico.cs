using ControleRemessaModelo.Negocio.DTOs;

namespace ControleRemessaModelo.Negocio.Interfaces
{
    public interface IUsuarioServico
    {
        UsuarioDTO? GetUsuarioLogin(string userName);
    }
}
