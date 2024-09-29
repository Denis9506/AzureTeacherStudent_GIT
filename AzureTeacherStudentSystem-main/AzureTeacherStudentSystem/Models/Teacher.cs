namespace AzureTeacherStudentSystem.Models
{
    public class Teacher : User
    {
        public virtual ICollection<Subject> Subjects { get; set; }

        public int Salary { get; set; }
    }
}
