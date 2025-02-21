namespace Proyecto_lenguajes.Models.Entities
{
    public class Professor
    {
        private string id;
        private string name;
        private string lastName;
        private string password;
        private string email;
        private string photo;
        private string expertise;

        public Professor(string id, string name, string lastName, string password, string email, string photo, string expertise)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Password = password;
            Email = email;
            Photo = photo;
            Expertise = expertise;
        }

        public Professor()
        {
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Expertise { get => expertise; set => expertise = value; }
    }
}
