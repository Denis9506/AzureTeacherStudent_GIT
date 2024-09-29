using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public IndexModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Subject = await _context.Subjects.ToListAsync();
        }
    }
}
