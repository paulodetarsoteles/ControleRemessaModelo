using ControleRemessaModelo.Entidades.Models;

namespace ControleRemessaModelo.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> GetAll();
        Usuario GetById(int id);
        Usuario GetByUserName(string userName);
        bool Insert(Usuario usuario);
        int Update(Usuario usuario);
        int Delete(int id);

        // Teste de Login
        Usuario? GetUsuarioLogin(string userName);
    }
}
