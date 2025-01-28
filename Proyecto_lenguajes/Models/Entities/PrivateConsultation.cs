namespace Proyecto_lenguajes.Models.Entities
{
    public class PrivateConsultation
    {
        private string text;
        private Student student;
        private Professor professor;

        public PrivateConsultation()
        {
        }

        public PrivateConsultation(string text, Student student, Professor professor)
        {
            Text = text;
            Student = student;
            Professor = professor;
        }

        public string Text { get => text; set => text = value; }
        public Student Student { get => student; set => student = value; }
        public Professor Professor { get => professor; set => professor = value; }
    }
}
