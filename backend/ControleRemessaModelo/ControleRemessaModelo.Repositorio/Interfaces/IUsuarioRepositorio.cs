using ControleRemessaModelo.Entidades.Models;

namespace ControleRemessaModelo.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario>? Buscar();
        Usuario BuscarPorId(int id);
        Usuario BuscarPorNome(string userName);
        Usuario? BuscarPorLogin(string login);
        bool Inserir(Usuario usuario);
        int Atualizar(Usuario usuario);
        int Excluir(int id);
    }
}
