using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class ProfessorServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public ProfessorServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Professor> Get()
        {

            List<Professor> professors = new List<Professor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Edu.GetAllTeachers", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read()) 
                    {
                        professors.Add(new Professor 
                                  {
                                    Id = sqlDataReader["Id"].ToString(),
                                    Name = sqlDataReader["Name"].ToString()
                                  });
                    }

                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return professors;
        }
    }
}
