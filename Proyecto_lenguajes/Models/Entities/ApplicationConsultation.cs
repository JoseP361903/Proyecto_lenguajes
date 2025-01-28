namespace Proyecto_lenguajes.Models.Entities
{
    public class ApplicationConsultation
    {

        private string text;
        private Student student;
        private Professor professor;

        public ApplicationConsultation()
        {
        }

        public ApplicationConsultation(string text, Student student, Professor professor)
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
