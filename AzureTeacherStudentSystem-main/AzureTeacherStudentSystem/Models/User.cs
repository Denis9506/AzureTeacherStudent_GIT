using System.ComponentModel.DataAnnotations.Schema;

namespace AzureTeacherStudentSystem.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string FullName => $"{LastName} {FirstName}".Trim();
    }
}
