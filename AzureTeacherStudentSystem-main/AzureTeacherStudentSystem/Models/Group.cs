using AzureTeacherStudentSystem.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Student> Students { get; set; }
    public bool IsArchived { get; set; } = false;
    public DateTime? ArchiveDate { get; set; }
}
