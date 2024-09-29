using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;
using System.Threading.Tasks;

namespace AzureTeacherStudentSystem.Pages.Groups
{
    public class RestoreModel : PageModel
    {
        private readonly DataContext _context;

        public RestoreModel(DataContext context)
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

            Group.IsArchived = false;
            Group.ArchiveDate = null;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
