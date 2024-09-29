namespace AzureTeacherStudentSystem.Models
{
    public class Student : User
    {
        public virtual Group Group { get; set; }
        
    }
}
