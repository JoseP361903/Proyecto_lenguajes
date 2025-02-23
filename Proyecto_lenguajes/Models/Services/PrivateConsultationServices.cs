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
                    SqlCommand sqlCommand = new SqlCommand("Edu.SendPrivateConsultation", connection);
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

        public List<PrivateConsultation> GetPrivateByStudent(string idStudent)
        {
            List<PrivateConsultation> consultations = new List<PrivateConsultation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Edu.GetPrivateConsultationByStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("Id", idStudent);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        PrivateConsultation consultation = new PrivateConsultation()
                        {
                            Id = (int)sqlDataReader["Id"],
                            Text = sqlDataReader["Text"].ToString(),
                            Status = (short)sqlDataReader["Status"],
                            Answer = sqlDataReader["Answer"].ToString(),
                            Date = DateOnly.FromDateTime(sqlDataReader.GetDateTime(6)),
                            Student = new Student()
                            {
                                Id = idStudent
                            },
                            Professor = new Professor()
                            {
                                Id = sqlDataReader["Id_Professor"].ToString(),
                                Name = sqlDataReader["Name"].ToString(),
                                LastName = sqlDataReader["LastName"].ToString(),
                                Photo = sqlDataReader["Photo"].ToString()
                            }
                        };
                        consultations.Add(consultation);
                    }
                    connection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return consultations;
        }

        public PrivateConsultation GetPrivateById(int id)
        {
            PrivateConsultation consultation = new PrivateConsultation();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Edu.GetPrivateConsultationById", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("Id", id);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        consultation = new PrivateConsultation()
                        {
                            Id = id,
                            Text = sqlDataReader.GetString(1),
                            Status = (short)sqlDataReader["Status"],
                            Answer = sqlDataReader.GetString(3),
                            Date = DateOnly.FromDateTime(sqlDataReader.GetDateTime(6)),
                            Student = new Student()
                            {
                                Id = sqlDataReader.GetString(4)
                            },
                            Professor = new Professor()
                            {
                                Id = sqlDataReader.GetString(5),
                                Name = sqlDataReader.GetString(7),
                                LastName = sqlDataReader.GetString(8),
                                Photo = sqlDataReader.GetString(9)
                            }
                        };

                    }
                    connection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return consultation;
        }
    }
}
