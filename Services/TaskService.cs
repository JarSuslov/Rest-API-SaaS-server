using Microsoft.EntityFrameworkCore;
using TodoSaaS.Data;
using TodoSaaS.DTOs.Tasks;
using TodoSaaS.Models;
using TodoSaaS.Services.Interfaces;

namespace TodoSaaS.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskResponse>> GetUserTasksAsync(Guid userId)
    {
        return await _db.Tasks
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => MapToResponse(t))
            .ToListAsync();
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, Guid userId)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        return task is null ? null : MapToResponse(task);
    }

    public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, Guid userId)
    {
        var task = new TodoTask
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Status = TaskItemStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return MapToResponse(task);
    }

    public async Task<TaskResponse?> UpdateTaskAsync(Guid taskId, UpdateTaskRequest request, Guid userId)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        if (task is null) return null;

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;

        await _db.SaveChangesAsync();

        return MapToResponse(task);
    }

    public async Task<bool> DeleteTaskAsync(Guid taskId, Guid userId)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        if (task is null) return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<List<TaskResponse>> GetAllTasksAsync()
    {
        return await _db.Tasks
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => MapToResponse(t))
            .ToListAsync();
    }

    private static TaskResponse MapToResponse(TodoTask task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        CreatedAt = task.CreatedAt,
        UserId = task.UserId
    };
}
