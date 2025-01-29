namespace Proyecto_lenguajes.Models.Entities
{
    public class CommentNew
    {
        private string content;
        private string idNew;
        private string idUser;
        private string Name;
        private Byte[] Photo;

        public CommentNew() { }

        public CommentNew(string content, string idNew, string idUser, byte[] photo, string name)
        {
            Content = content;
            IdNew = idNew;
            IdUser = idUser;
            Photo = photo;
            Name = name;
        }

        public string Content { get => content; set => content = value; }
        public string IdNew { get => idNew; set => idNew = value; }
        public string IdUser { get => idUser; set => idUser = value; }
        public byte[] Photo1 { get => Photo; set => Photo = value; }
        public string Name1 { get => Name; set => Name = value; }
    }
}
