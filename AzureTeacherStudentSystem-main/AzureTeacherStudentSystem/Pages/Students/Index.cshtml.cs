using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;
using System.Diagnostics;
using AzureTeacherStudentSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzureTeacherStudentSystem.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(DataContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<StudentDTO> Student { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string LastNameFilter { get; set; }


        public async Task OnGetAsync()
        {
            var query = _context.Students.Include(s => s.Group).AsQueryable();

            if (!string.IsNullOrEmpty(NameFilter))
            {
                query = query.Where(s => s.FirstName.Contains(NameFilter));
            }

            if (!string.IsNullOrEmpty(LastNameFilter))
            {
                query = query.Where(s => s.LastName.Contains(LastNameFilter));
            }

            Student = await query.Select(s => new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                GroupName = s.Group.Name
            }).ToListAsync();
        }

    }
}
