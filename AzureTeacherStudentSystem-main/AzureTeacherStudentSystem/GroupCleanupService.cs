using AzureTeacherStudentSystem.Data;
using Microsoft.EntityFrameworkCore;

public class GroupCleanupService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;

    public GroupCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DeleteArchivedGroups, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private async void DeleteArchivedGroups(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var groupsToDelete = await _context.Groups
                .Where(g => g.IsArchived && g.ArchiveDate.HasValue && g.ArchiveDate.Value.AddDays(14) <= DateTime.UtcNow)
                .ToListAsync();

            _context.Groups.RemoveRange(groupsToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
