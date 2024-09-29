using AzureTeacherStudentSystem.Models;

public class Homework
{
	public int Id { get; set; } 
	public string Topic { get; set; }
	public Lesson Lesson { get; set; }
	public Student Student { get; set; }
}
