using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class CommentCourseServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public CommentCourseServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Post(CommentCourse commentCourse)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Edu.AddCommentCourse", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Acronym", commentCourse.Acronym);
                    sqlCommand.Parameters.AddWithValue("@Content", commentCourse.Content);
                    sqlCommand.Parameters.AddWithValue("@Id_User", commentCourse.IdUser);
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

        public List<CommentCourse> Get(string id)
        {

            List<CommentCourse> courses = new List<CommentCourse>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Edu.GetCommentCoursesById", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Acronym", id);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        courses.Add(new CommentCourse
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

            return courses;
        }
    }
}
