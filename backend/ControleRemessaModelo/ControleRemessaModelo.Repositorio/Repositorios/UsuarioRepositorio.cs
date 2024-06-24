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
                UserName = "paulo",
                Password = "Paulo@123",
                Nome = "Paulo de Tarso",
                Role = "admin",
                Email = "paulo@teste.com",
                Telefone = "85991672946"
            };

            if (string.IsNullOrEmpty(userName) || userName != resultado.UserName)
                return null;

            return resultado;
        }
    }
}
