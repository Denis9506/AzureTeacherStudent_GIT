using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Teachers
{
    public class CreateModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public CreateModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
            Subjects = new();
        }

        public IActionResult OnGet()
        {
            Subjects = _context.Subjects.ToList();
            return Page();
        }

        public List<Subject> Subjects { get; set; }

        [BindProperty]
        public Teacher Teacher { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int subject)
        {
            Teacher.Subjects = new List<Subject>()
            {
                _context.Subjects.Find(subject)
            };

            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
