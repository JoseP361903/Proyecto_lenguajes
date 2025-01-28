using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class PrivateConsultationServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public PrivateConsultationServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Post(PrivateConsultation privateConsultation) {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SendPrivateConsultation", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Text", privateConsultation.Text);
                    sqlCommand.Parameters.AddWithValue("@Id_Student", privateConsultation.Student.Id);
                    sqlCommand.Parameters.AddWithValue("@Id_Professor", privateConsultation.Professor.Id);

                    result = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                } catch (SqlException) 
                {
                    throw;
                }
            }

                return result;
        }
    }
}
