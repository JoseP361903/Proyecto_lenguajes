using Proyecto_lenguajes.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Proyecto_lenguajes.Models.Services
{
    public class StudentServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentServices(IConfiguration configuration) { 
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Post(Student student) 
        { 
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("AddNewStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                    sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                    sqlCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", student.Password);
                    sqlCommand.Parameters.AddWithValue("@Likings", student.Likings);
                    sqlCommand.Parameters.AddWithValue("@Photo", student.Photo);

                    result = sqlCommand.ExecuteNonQuery();
                    connection.Close();

                } catch (SqlException) {
                    throw;
                }
            }
            return result;
        }

        public Student ValidateID(string Id) 
        { 
        
            Student student = new Student();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("ValidateID", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        student.Id = sqlDataReader.GetString(0);
                        student.Name = sqlDataReader.GetString(1);
                    }
                    connection.Close();
                }catch (SqlException) 
                { 
                    throw; 
                }
            }

            return student;
        }

        public int Put(Student student) 
        { 
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("UpdateStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                    sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                    sqlCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", student.Password);
                    sqlCommand.Parameters.AddWithValue("@Likings", student.Likings);
                    sqlCommand.Parameters.AddWithValue("@Photo", student.Photo);

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
