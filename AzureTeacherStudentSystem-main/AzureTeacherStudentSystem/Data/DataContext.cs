using AzureTeacherStudentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureTeacherStudentSystem.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)   
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
}
