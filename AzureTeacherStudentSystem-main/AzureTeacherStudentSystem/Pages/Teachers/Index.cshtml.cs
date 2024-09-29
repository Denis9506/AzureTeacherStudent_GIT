using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string LastNameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? SalaryMin { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? SalaryMax { get; set; }

        public IList<Teacher> Teacher { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Teachers.Include(x => x.Subjects).AsQueryable();

            if (!string.IsNullOrEmpty(NameFilter))
            {
                query = query.Where(t => t.FirstName.Contains(NameFilter));
            }

            if (!string.IsNullOrEmpty(LastNameFilter))
            {
                query = query.Where(t => t.LastName.Contains(LastNameFilter));
            }

            if (SalaryMin.HasValue)
            {
                query = query.Where(t => t.Salary >= SalaryMin);
            }

            if (SalaryMax.HasValue)
            {
                query = query.Where(t => t.Salary <= SalaryMax);
            }

            Teacher = await query.ToListAsync();
        }

    }
}
