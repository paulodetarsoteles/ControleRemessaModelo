using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Repositorio.Interfaces;

namespace ControleRemessaModelo.Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public List<Usuario> GetAll()
        {
            List<Usuario> resultado = [];

            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return resultado;
        }

        public Usuario GetById(int id)
        {
            Usuario usuario = new();
            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return usuario;
        }

        public Usuario GetByUserName(string userName)
        {
            Usuario resultado = new();

            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return resultado;
        }

        public bool Insert(Usuario usuario)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public int Update(Usuario usuario)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return 1;
        }

        public int Delete(int id)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }

            return 1;
        }

        // Teste de Login
        public Usuario GetUsuarioLogin(string userName)
        {
            Usuario resultado = new()
            {
                UserName = userName,
                Password = "",
                Nome = "Teste Ok",
                Role = "admin",
                Email = "teste@teste.com",
                Telefone = "85991672946"
            };

            return resultado;
        }
    }
}
