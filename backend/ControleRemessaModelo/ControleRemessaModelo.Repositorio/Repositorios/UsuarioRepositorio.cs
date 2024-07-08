using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Repositorio.DataConnection;
using ControleRemessaModelo.Repositorio.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data;

namespace ControleRemessaModelo.Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ConnectionSetting _connection;

        public UsuarioRepositorio(IOptions<ConnectionSetting> connection) => _connection = connection.Value;

        public List<Usuario> Buscar()
        {
            List<Usuario> resultado = [];
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return resultado;
        }

        public Usuario BuscarPorId(int id)
        {
            Usuario resultado = new();
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return resultado;
        }

        public Usuario BuscarPorNome(string userName)
        {
            Usuario resultado = new();
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return resultado;
        }

        public Usuario? BuscarPorLogin(string login)
        {
            Usuario resultado = new();
            using var connection = new SqliteConnection(_connection.SQLiteConnection);

            try
            {
                connection.Open();

                string sql = @"SELECT 
                                   u.Id, u.Login, u.Senha, u.Nome, u.Role, u.Email, u.Telefone 
                               FROM 
                                   tb_Usuarios AS u 
                               WHERE 1 = 1  
                                   AND u.Login LIKE @Login 
                               ORDER BY 1 DESC 
                               LIMIT 1; ";

                SqliteCommand command = new(sql, connection);

                command.Parameters.AddWithValue("Login", login);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    resultado.Id = reader.GetInt32(0);
                    resultado.Login = reader.GetString(1);
                    resultado.Senha = reader.GetString(2);
                    resultado.Nome = reader.GetString(3);
                    resultado.Role = reader.GetString(4);
                    resultado.Email = reader.GetString(5);

                    if (!reader.IsDBNull(6))
                        resultado.Telefone = reader.GetString(6);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return resultado;
        }

        public bool Inserir(Usuario usuario)
        {
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return true;
        }

        public int Atualizar(Usuario usuario)
        {
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return 1;
        }

        public int Excluir(int id)
        {
            IDbConnection connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {DateTime.Now} - {ex.Message}");
                throw new Exception("Erro ao acessar dados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return 1;
        }
    }
}
