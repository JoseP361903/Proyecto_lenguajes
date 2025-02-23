using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;
using System.Reflection.PortableExecutable;

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
                    SqlCommand sqlCommand = new SqlCommand("Edu.SendAppointment", connection);
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

        public List<ApplicationConsultation> GetByStudent(string idStudent)
        {
            List<ApplicationConsultation> consultations = new List<ApplicationConsultation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Edu.GetApplicationConsultationByStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("Id", idStudent);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        ApplicationConsultation consultation = new ApplicationConsultation()
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
                }catch (Exception)
                {
                    throw;
                }
            }
            return consultations;
        }

        public ApplicationConsultation GetById(int id)
        {
            ApplicationConsultation consultation = new ApplicationConsultation();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Edu.GetApplicationConsultationById", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("Id", id);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                      consultation = new ApplicationConsultation()
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
