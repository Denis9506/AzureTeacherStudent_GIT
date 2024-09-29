using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public CreateModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
            Groups = new();
        }

        public List<Group> Groups { get; set; }

        public IActionResult OnGet()
        {
            Groups = _context.Groups.ToList();
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int group)
        {
            Student.Group = _context.Groups.Find(group);
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
