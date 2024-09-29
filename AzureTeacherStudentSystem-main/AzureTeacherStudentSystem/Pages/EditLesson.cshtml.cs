using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;
using Azure.Storage.Blobs;
using Azure.Data.Tables;
using AzureTeacherStudentSystem.DTOs;

namespace AzureTeacherStudentSystem.Pages
{
    public class EditLessonModel : PageModel
    {
        private readonly DataContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly TableServiceClient _tableServiceClient;

        public EditLessonModel(DataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        [BindProperty]
        public Lesson Lesson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(x => x.Schedule)
                .ThenInclude(x => x.Group)
                .ThenInclude(x => x.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            Lesson = lesson;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile? file)
        {
            var lessonToUpdate = await _context.Lessons
                .Include(x => x.Schedule)
                .ThenInclude(x => x.Group)
                .ThenInclude(x => x.Students)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lessonToUpdate == null)
            {
                return NotFound();
            }

            if (file != null && file.Length > 0)
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("homework-files");
                await blobContainer.CreateIfNotExistsAsync();
                var blobClient = blobContainer.GetBlobClient(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                lessonToUpdate.Homework = blobClient.Uri.ToString();
            }

            foreach (var student in lessonToUpdate.Schedule.Group.Students)
            {
                var homework = new Homework()
                {
                    Topic = lessonToUpdate.Topic,
                    Student = student,
                    Lesson = lessonToUpdate
                };

                _context.Homeworks.Add(homework);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
