using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;
using System.Diagnostics;
using AzureTeacherStudentSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IList<StudentDTO> Student { get; set; } = default!;
        public SelectList Groups { get; set; }  

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LastNameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GroupIdFilter { get; set; }  

        public async Task OnGetAsync()
        {
            Groups = new SelectList(await _context.Groups.ToListAsync(), "Id", "Name");

            var query = _context.Students.Include(s => s.Group).AsQueryable();

            if (!string.IsNullOrEmpty(NameFilter))
            {
                query = query.Where(s => s.FirstName.Contains(NameFilter));
            }

            if (!string.IsNullOrEmpty(LastNameFilter))
            {
                query = query.Where(s => s.LastName.Contains(LastNameFilter));
            }

            if (GroupIdFilter.HasValue)
            {
                query = query.Where(s => s.Group.Id == GroupIdFilter.Value);
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
