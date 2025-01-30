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

        //GetNewsById
        public BreakingNew Get(string idNot)
        {
            BreakingNew breakingNew = new BreakingNew();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetNewsById", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdNot", idNot);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        breakingNew.IdNot = reader.GetString(0);
                        breakingNew.Date = DateOnly.FromDateTime(reader.GetDateTime(1));
                        breakingNew.Title = reader.GetString(2);
                        breakingNew.Paragraph = reader.GetString(3);

                        // Manejar el caso en que Photo es NULL en la base de datos
                        if (!reader.IsDBNull(4))
                        {
                            breakingNew.Photo = Encoding.ASCII.GetBytes(reader.GetString(4));
                        }
                        else
                        {
                            breakingNew.Photo = new byte[0]; // Asignar un array vacío si es NULL
                        }
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


        //GetMaxNewsId
        public BreakingNew GetMaxId()
        {
            BreakingNew breakingNew = new BreakingNew();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetMaxNewsId", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        breakingNew.IdNot = reader.GetString(0);
                        breakingNew.Date = DateOnly.FromDateTime(reader.GetDateTime(1));
                        breakingNew.Title = reader.GetString(2);
                        breakingNew.Paragraph = reader.GetString(3);
                        breakingNew.Photo = Encoding.ASCII.GetBytes(reader.GetString(4));
                    }

                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return breakingNew;
        }

    }
}
