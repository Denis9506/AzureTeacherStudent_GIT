using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;
using System.Threading.Tasks;

namespace AzureTeacherStudentSystem.Pages.Groups
{
    public class ArchiveModel : PageModel
    {
        private readonly DataContext _context;

        public ArchiveModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Group = await _context.Groups.FindAsync(id);
            if (Group == null)
            {
                return NotFound();
            }

            Group.IsArchived = true;
            Group.ArchiveDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
