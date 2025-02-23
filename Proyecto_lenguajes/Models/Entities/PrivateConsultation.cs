namespace Proyecto_lenguajes.Models.Entities
{
    public class PrivateConsultation
    {
        private int id;
        private string text;
        private short status;
        private string answer;
        private DateOnly date;
        private Student student;
        private Professor professor;
        

        public PrivateConsultation()
        {
        }

        public PrivateConsultation(int id, string text, short status, string answer, DateOnly date, Student student, Professor professor)
        {
            Id = id;
            Text = text;
            Status = status;
            Answer = answer;
            Date = date;
            Student = student;
            Professor = professor;
        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public short Status { get => status; set => status = value; }
        public string Answer { get => answer; set => answer = value; }
        public DateOnly Date { get => date; set => date = value; }
        public Student Student { get => student; set => student = value; }
        public Professor Professor { get => professor; set => professor = value; }
    }
}
