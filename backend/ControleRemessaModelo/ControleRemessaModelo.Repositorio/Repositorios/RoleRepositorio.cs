using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Repositorio.DataConnection;
using ControleRemessaModelo.Repositorio.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data;

namespace ControleRemessaModelo.Repositorio.Repositorios
{
    public class RoleRepositorio : IRoleRepositorio
    {
        private readonly ConnectionSetting _connection;

        public RoleRepositorio(IOptions<ConnectionSetting> connection) => _connection = connection.Value;

        public List<Role>? Buscar()
        {
            List<Role> resultado = new();
            using var connection = new SqliteConnection(_connection.SQLiteConnection);

            try
            {
                connection.Open();

                string sql = @" SELECT 
                                    r.Id, r.Nome  
                                FROM tb_Roles AS r 
                                WHERE 1 = 1 
                                ORDER BY 1 DESC
                                LIMIT 10; 
                            ";

                SqliteCommand command = new(sql, connection);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    resultado.Add(new Role
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1)
                    });
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
    }
}
