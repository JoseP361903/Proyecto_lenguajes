namespace Proyecto_lenguajes.Models.Entities
{
    public class Course
    {
        private string acronym;
        private string name;
        private string description;
        private int cycle;
        private int semester;
        private int quota;
        private Professor professor;

        public Course()
        {
        }

        public Course(string acronym, string name, string description, int cycle, int semester, int quota, Professor professor)
        {
            Acronym = acronym;
            Name = name;
            Description = description;
            Cycle = cycle;
            Semester = semester;
            Quota = quota;
            Professor = professor;
        }

        public string Acronym { get => acronym; set => acronym = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Cycle { get => cycle; set => cycle = value; }
        public int Semester { get => semester; set => semester = value; }
        public int Quota { get => quota; set => quota = value; }
        public Professor Professor { get => professor; set => professor = value; }
    }
}
