using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Groups
{
    public class EditModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public EditModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.Include(x => x.Students).FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            Group = group;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var groupToUpdate = await _context.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.Id == Group.Id);

            if (groupToUpdate == null)
            {
                return NotFound();
            }

            groupToUpdate.Name = Group.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(Group.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }



        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
