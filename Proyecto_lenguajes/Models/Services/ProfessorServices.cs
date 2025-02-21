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

        public Professor Get(string id)
        {
            Professor professor = new Professor();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetProfessorById", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        professor.Id = id;
                        professor.Name = reader.GetString(1);
                        professor.LastName = reader.GetString(2);
                        professor.Password = reader.GetString(3);
                        professor.Email = reader.GetString(4);
                        professor.Photo = reader.GetString(6);
                    }
                    sqlConnection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return professor;
        }
    }
}
