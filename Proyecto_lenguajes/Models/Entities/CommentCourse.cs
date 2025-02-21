using System.Resources;

namespace Proyecto_lenguajes.Models.Entities
{
    public class CommentCourse
    {

        private string content;
        private string acronym;
        private string idUser;
        private DateOnly date;
       

        public CommentCourse() { }

        public CommentCourse(string content, string idUser, string acronym)
        {
            Content = content;
            IdUser = idUser;
            Acronym = acronym;
            Date = date;
        }

        public string Content { get => content; set => content = value; }
        public string IdUser { get => idUser; set => idUser = value; }
        public string Acronym { get => acronym; set => acronym = value; }
        public DateOnly Date { get => date; set => date = value; }
    }
}
