namespace Proyecto_lenguajes.Models.Entities
{
    public class BreakingNew
    {
        private string idNot;
        private DateOnly date;
        private string title;
        private string paragraph;
        private byte[] photo;

        public BreakingNew() { }

        public BreakingNew(string idNot, DateOnly date, string title, string paragraph, byte[] photo)
        {
            IdNot = idNot;
            Date = date;
            Title = title;
            Paragraph = paragraph;
            Photo = photo;
        }

        public string IdNot { get => idNot; set => idNot = value; }
        public DateOnly Date { get => date; set => date = value; }
        public string Title { get => title; set => title = value; }
        public string Paragraph { get => paragraph; set => paragraph = value; }
        public byte[] Photo { get => photo; set => photo = value; }
    }
}
