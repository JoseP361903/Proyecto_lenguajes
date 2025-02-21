namespace Proyecto_lenguajes.Models.Entities
{
    public class CommentNew
    {
        private string content;
        private int idNew;
        private string idUser;
        private DateOnly date;
        private int newIdCommentN;

        public CommentNew() { }

        public CommentNew(int idNew, string idUser, string content, DateOnly date, int newIdCommentN)
        {
            IdNew = idNew;
            IdUser = idUser;
            Content = content;
            Date = date;
            NewIdCommentN = newIdCommentN;
        }

        public string Content { get => content; set => content = value; }
        public int IdNew { get => idNew; set => idNew = value; }
        public string IdUser { get => idUser; set => idUser = value; }
        public DateOnly Date { get => date; set => date = value; }
        public int NewIdCommentN { get => newIdCommentN; set => newIdCommentN = value; }
    }
}