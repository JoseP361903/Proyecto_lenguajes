using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class CommentNewServices
    {

        private readonly IConfiguration _configuration;
        string connectionString;

        public CommentNewServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Post(CommentNew commentNew) { 
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Edu.AddCommentNew", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id_New", commentNew.IdNew);
                    sqlCommand.Parameters.AddWithValue("@Content", commentNew.Content);
                    sqlCommand.Parameters.AddWithValue("@Id_User", commentNew.IdUser);
                    result = sqlCommand.ExecuteNonQuery();

                    connection.Close();

                } catch (SqlException) 
                {
                    throw;
                }
            
            }

            return result;
        }

        public List<CommentNew> Get(string id) 
        { 
        
            List<CommentNew> news = new List<CommentNew>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Edu.GetCommentNewsById", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@IdNot", id);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        news.Add(new CommentNew
                        {
                            Content = sqlDataReader["ContentC"].ToString(),
                            IdUser = sqlDataReader["Id_User"].ToString(),
                            Date = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["Date"]))
                        });
                    }

                    connection.Close();

                }
                catch (SqlException)

                {
                    throw;
                }

            }

            return news;
        }


        public IEnumerable<CommentNew> GetAll(int idNew)
        {
            List<CommentNew> commentsList = new List<CommentNew>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Edu.GetAllCommentsByNewsId", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdNew", idNew);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CommentNew comment = new CommentNew(
                            reader.GetInt32(0), // IdNew
                            reader.GetString(1), // IdUser
                            reader.GetString(2), // Content
                            DateOnly.FromDateTime(reader.GetDateTime(3)), // Date
                            reader.GetInt32(4) // NewIdCommentN
                        );
                        commentsList.Add(comment);
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener los comentarios", ex);
                }
            }

            return commentsList;
        }


        public Student GetStudentCommentData(string id)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Edu.GetStudentCommentData", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Photo = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }

                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return student;
        }

        public Professor GetProfessorCommentData(string id)
        {
            Professor professor = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Edu.GetProfessorCommentData", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        professor = new Professor
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Photo = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }

                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return professor;
        }


        public int CheckType(string id)
        {
            int type = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Edu.CheckCommentType", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        type = reader.GetInt32(0);
                    }

                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return type;
        }

    }
}
