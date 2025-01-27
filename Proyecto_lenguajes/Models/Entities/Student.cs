namespace Proyecto_lenguajes.Models.Entities
{
    public class Student
    {
        private String id;
        private String name;
        private String lastName;
        private String password;
        private String email;

        public Student()
        {
        }

        public Student(string id, string name, string lastName, string password, string email)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Password = password;
            Email = email;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
    }
}
