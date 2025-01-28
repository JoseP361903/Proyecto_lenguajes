using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class ApplicationConsultationServices
    {

        private readonly IConfiguration _configuration;
        string connectionString;

        public ApplicationConsultationServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Post(ApplicationConsultation applicationConsultation)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SendPrivateConsultation", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Text", applicationConsultation.Text);
                    sqlCommand.Parameters.AddWithValue("@Id_Student", applicationConsultation.Student.Id);
                    sqlCommand.Parameters.AddWithValue("@Id_Professor", applicationConsultation.Professor.Id);

                    result = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return result;
        }

    }
}
