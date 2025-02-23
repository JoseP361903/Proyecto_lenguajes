using Proyecto_lenguajes.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;
using Proyecto_lenguajes.Util;

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

        //GetStudentById
        public Student Get(string id)
        {
            Student student = new Student();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetStudentByID", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        student.Id = id;
                        student.Name = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.Password = reader.GetString(3);
                        student.Email = reader.GetString(4);
                        student.Likings = reader.GetString(5);
                        student.Photo = reader.GetString(6);
                    }
                    sqlConnection.Close();
                } catch (SqlException)
                {
                    throw;
                }

                
            }

            return student;
        }

        //LoginAuthentication
        //This methods receives as a parameter an student built only by id and password, and authenticates the existence
        //The return cases will show an option on the controller
        public int Authenticate(Student student)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Edu.LoginAuthentication", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Password", student.Password);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.ParameterName = "@RETURN_VALUE";
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    command.ExecuteNonQuery();

                    result = (int)returnValue.Value;
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return result;
        }


        public int Post(Student student) 
        { 
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Edu.AddNewStudent", connection);
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

                    EmailSender sender = new EmailSender();
                    sender.SendEmail(student, "Bienvenido a la plataforma");

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
                    SqlCommand sqlCommand = new SqlCommand("Edu.ValidateID", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        student.Id = sqlDataReader["Id"].ToString();
                        student.Name = sqlDataReader["Name"].ToString();
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

                    SqlCommand sqlCommand = new SqlCommand("Edu.UpdateStudent", connection);
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
