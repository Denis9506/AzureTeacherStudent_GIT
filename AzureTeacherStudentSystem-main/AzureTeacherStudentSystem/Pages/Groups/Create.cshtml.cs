using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Groups
{
    public class CreateModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public CreateModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Groups.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
