namespace Proyecto_lenguajes.Models.Entities
{
    public class CommentNew
    {
        private string content;
        private string idNew;
        private string idUser;
        private DateOnly date;

        public CommentNew() { }

        public CommentNew(string content, string idNew, string idUser, string name, DateOnly date)
        {
            Content = content;
            IdNew = idNew;
            IdUser = idUser;
            Date = date;
        }

        public string Content { get => content; set => content = value; }
        public string IdNew { get => idNew; set => idNew = value; }
        public string IdUser { get => idUser; set => idUser = value; }
        public DateOnly Date { get => date; set => date = value; }
    }
}
