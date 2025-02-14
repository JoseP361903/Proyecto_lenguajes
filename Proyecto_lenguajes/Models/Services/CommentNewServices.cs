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

        public List<CommentNew> Get(string id) { 
        
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
                            Name1 = sqlDataReader["Name"].ToString(),
                            Photo1 = sqlDataReader["Photo"] != DBNull.Value ? (byte[])sqlDataReader["Photo"] : null
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

    }
}
