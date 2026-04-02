using client.DTOs;
using client.Enums;

namespace client.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();

    public Task<TaskResponseDto?> GetTaskByIdAsync(Guid id);

    public Task<bool> CreateTaskAsync(TaskCreateDto createDto);

    public Task<bool> UpdateTaskAsync(Guid id, TaskUpdateDto updateDto);

    public Task<bool> UpdateStatusAsync(Guid id, StatusTask status);

    public Task<bool> UpdatePriorityAsync(Guid id, PriorityTask priority);

    public Task<bool> DeleteTaskAsync(Guid id);
}
