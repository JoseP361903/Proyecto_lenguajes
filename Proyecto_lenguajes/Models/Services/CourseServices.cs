using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;

namespace Proyecto_lenguajes.Models.Services
{
    public class CourseServices
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public CourseServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        //GetCoursesByCycle
        public List<Course> Get(int cycle)
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Edu.GetCoursesByCycle", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Cycle", cycle);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            Acronym = reader["Acronym"].ToString(),
                            Name = reader["Name"].ToString()
                        });
                    }

                    connection.Close();
                
                } catch (SqlException)
                {
                    throw;
                }
            }
            return courses;
        }

        //GetCoursesByCycle
        public Course Get(string acronym)
        {
            Course course = new Course();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Edu.GetCourseByAcronym", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Acronym", acronym);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        course = new Course
                        {
                            Acronym = reader["Acronym"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Cycle = Convert.ToInt32(reader["Cycle"]),
                            Semester = Convert.ToInt32(reader["Semester"]),
                            Quota = Convert.ToInt32(reader["Quota"]),
                            Professor = new Professor
                            {
                                Id = reader["Id_Professor"].ToString(),
                                Name = reader["Name_Professor"].ToString(),
                                LastName = reader["LastName"].ToString()
                            }
                        };
                    }
                    connection.Close();

                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return course;
        }


    }
}
