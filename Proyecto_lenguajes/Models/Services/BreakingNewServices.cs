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

        public IEnumerable<BreakingNew> GetAllNews()
        {
            List<BreakingNew> newsList = new List<BreakingNew>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetAllNews", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        BreakingNew breakingNew = new BreakingNew(
                            reader.GetInt32(4), // idNew
                            reader.GetString(1), // Title
                            reader.GetString(2), // Paragraph
                            reader.IsDBNull(3) ? null : reader.GetString(3), // Photo
                            DateOnly.FromDateTime(reader.GetDateTime(0)) // Date
                        );
                        newsList.Add(breakingNew);
                    }


                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las noticias", ex);
                }
            }

            return newsList;
        }
    }
}