using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Groups
{
    public class IndexModel : PageModel
    {
        private readonly AzureTeacherStudentSystem.Data.DataContext _context;

        public IndexModel(AzureTeacherStudentSystem.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Group = await _context.Groups.ToListAsync();
        }
    }
}
