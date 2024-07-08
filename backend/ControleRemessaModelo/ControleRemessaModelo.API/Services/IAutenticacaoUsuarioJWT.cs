using ControleRemessaModelo.Negocio.DTOs;

namespace ControleRemessaModelo.API.Services
{
    public interface IAutenticacaoUsuarioJWT
    {
        string GenerateToken(UsuarioLoginDTO userLogin);
        string GetHashMd5(string input);
    }
}
