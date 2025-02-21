using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;
using System.Text;

namespace Proyecto_lenguajes.Models.Services
{
    public class BreakingNewServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public BreakingNewServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // GetNewsById
        public BreakingNew Get(int idNot)
        {
            BreakingNew breakingNew = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetNewsById", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdNot", idNot);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        breakingNew = new BreakingNew
                        {
                            IdNot = reader.GetInt32(0).ToString(),
                            Date = DateOnly.FromDateTime(reader.GetDateTime(1)),
                            Title = reader.GetString(2),
                            Paragraph = reader.GetString(3),
                            Photo = reader.GetString(4)
                        };
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener la noticia", ex);
                }
            }

            return breakingNew;
        }

        // GetMaxNewsId
        public BreakingNew GetMaxId()
        {
            BreakingNew breakingNew = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetMaxNewsId", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        breakingNew = new BreakingNew
                        {
                            IdNot = reader.GetInt32(0).ToString(),
                            Date = DateOnly.FromDateTime(reader.GetDateTime(1)),
                            Title = reader.GetString(2),
                            Paragraph = reader.GetString(3),
                            Photo = reader.GetString(4)
                        };
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID máximo de la noticia", ex);
                }
            }

            return breakingNew;
        }
    }
}