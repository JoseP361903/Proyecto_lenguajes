namespace Proyecto_lenguajes.Models.Entities
{
    public class ApplicationConsultation
    {

        private int id;
        private string text;
        private short status;
        private string answer;
        private DateOnly date;
        private Student student;
        private Professor professor;

        public ApplicationConsultation()
        {
        }

        public ApplicationConsultation(int id, string text, short status, DateOnly date, Student student, Professor professor, string answer)
        {
            Id = id;
            Text = text;
            Status = status;
            Date = date;
            Student = student;
            Professor = professor;
            Answer = answer;
        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public short Status { get => status; set => status = value; }
        public DateOnly Date { get => date; set => date = value; }
        public Student Student { get => student; set => student = value; }
        public Professor Professor { get => professor; set => professor = value; }
        public string Answer { get => answer; set => answer = value; }
    }
}
