using ControleRemessaModelo.Negocio.DTOs;

namespace ControleRemessaModelo.Negocio.Interfaces
{
    public interface IRoleServico
    {
        List<RoleDTO>? Buscar();
    }
}
