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

        public List<Usuario>? Buscar()
        {
            List<Usuario> resultado = [];
            using var connection = new SqliteConnection(_connection.SQLiteConnection);
            connection.Open();

            try
            {
                connection.Open();

                string sql = @" SELECT 
                                    u.Id, u.Login, u.Senha, u.Nome, u.Email, u.Telefone, u.Roles_Id 
                                FROM tb_Usuarios AS u 
                                WHERE 1 = 1 
                                ORDER BY 1 DESC
                                LIMIT 1000; 
                            ";

                SqliteCommand command = new(sql, connection);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    Usuario usuario = new()
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Senha = reader.GetString(2),
                        Nome = reader.GetString(3),
                        Email = reader.GetString(4),
                        Role_Id = reader.GetString(6)
                    };

                    if (!reader.IsDBNull(5))
                    {
                        usuario.Telefone = reader.GetString(5);
                    }

                    resultado.Add(usuario);
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

                string sql = @" SELECT 
                                    u.Id, u.Login, u.Senha, u.Nome, u.Email, u.Telefone, u.Roles_Id 
                                FROM tb_Usuarios AS u 
                                WHERE 1 = 1 
                                    AND u.Login LIKE @Login
                                ORDER BY 1 DESC
                                LIMIT 10; 
                            ";

                SqliteCommand command = new(sql, connection);

                command.Parameters.AddWithValue("Login", login);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    resultado.Id = reader.GetInt32(0);
                    resultado.Login = reader.GetString(1);
                    resultado.Senha = reader.GetString(2);
                    resultado.Nome = reader.GetString(3);
                    resultado.Email = reader.GetString(4);
                    resultado.Role_Id = reader.GetString(6);

                    if (!reader.IsDBNull(5))
                    {
                        resultado.Telefone = reader.GetString(5);
                    }
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
