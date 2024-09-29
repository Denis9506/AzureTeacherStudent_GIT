using Azure.Data.Tables;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.DTOs;
using AzureTeacherStudentSystem.Migrations;
using AzureTeacherStudentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzureTeacherStudentSystem.Pages
{
	public class HomeworkModel : PageModel
	{
		private readonly DataContext _context;

		public HomeworkModel(DataContext context)
		{
			_context = context;
		}

		public List<Homework> Homeworks { get; set; }

		public async Task OnGetAsync()
		{
			int studentId = 4; 
			Homeworks = await _context.Homeworks
				.Include(h => h.Student)
				.Include(h => h.Lesson)
				.Where(h => h.Student.Id == studentId)
				.ToListAsync();
		}
	}

}
