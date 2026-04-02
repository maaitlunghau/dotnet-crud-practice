using server.Enums;
using server.Models;

namespace server.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<TodoTask>> GetAllTasksAsync();

    public Task<TodoTask?> GetTaskByIdAsync(Guid? id);

    public Task<TodoTask?> CreateTaskAsync(TodoTask? task);

    public Task<TodoTask?> UpdateTaskAsync(TodoTask? task);

    public Task<TodoTask?> UpdateTaskStatusAsync(Guid? id, StatusTask status);

    public Task<TodoTask?> UpdateTaskPriorityAsync(Guid? id, PriorityTask priority);

    public Task DeleteTaskAsync(Guid? id);
}
