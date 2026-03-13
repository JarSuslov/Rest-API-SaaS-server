using TodoSaaS.DTOs.Tasks;

namespace TodoSaaS.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponse>> GetUserTasksAsync(Guid userId);
    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, Guid userId);
    Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, Guid userId);
    Task<TaskResponse?> UpdateTaskAsync(Guid taskId, UpdateTaskRequest request, Guid userId);
    Task<bool> DeleteTaskAsync(Guid taskId, Guid userId);
    Task<List<TaskResponse>> GetAllTasksAsync();
}
