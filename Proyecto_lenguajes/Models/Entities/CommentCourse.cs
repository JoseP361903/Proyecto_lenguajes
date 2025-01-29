namespace Proyecto_lenguajes.Models.Entities
{
    public class CommentCourse
    {

        private string content;
        private string acronym;
        private string idUser;
        private string Name;
        private Byte[] Photo;

        public CommentCourse() { }

        public CommentCourse(string content, string idUser, string acronym, string name, byte[] photo)
        {
            Content = content;
            IdUser = idUser;
            Acronym = acronym;
            Name = name;
            Photo = photo;
        }

        public string Content { get => content; set => content = value; }
        public string IdUser { get => idUser; set => idUser = value; }
        public string Acronym { get => acronym; set => acronym = value; }
        public string Name1 { get => Name; set => Name = value; }
        public byte[] Photo1 { get => Photo; set => Photo = value; }
    }
}
