using client.DTOs;
using client.Enums;
using client.Repositories;
using System.Net.Http.Json;

namespace client.Services;

public class TaskService : ITaskRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TaskService> _logger;

    public TaskService(IHttpClientFactory httpClientFactory, ILogger<TaskService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        _logger = logger;
    }


    public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TaskResponseDto>>("Task");
            return response ?? Enumerable.Empty<TaskResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get tasks via API");
            return Enumerable.Empty<TaskResponseDto>();
        }
    }

    public async Task<TaskResponseDto?> GetTaskByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<TaskResponseDto>($"Task/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get task {id} via API");
            return null;
        }
    }

    public async Task<bool> CreateTaskAsync(TaskCreateDto createDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Task", createDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create task via API");
            return false;
        }
    }

    public async Task<bool> UpdateTaskAsync(Guid id, TaskUpdateDto updateDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"Task/{id}", updateDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update task via API");
            return false;
        }
    }

    public async Task<bool> UpdateStatusAsync(Guid id, StatusTask status)
    {
        try
        {
            var response = await _httpClient.PatchAsJsonAsync($"Task/{id}/status", status);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update status for task {id}");
            return false;
        }
    }

    public async Task<bool> UpdatePriorityAsync(Guid id, PriorityTask priority)
    {
        try
        {
            var response = await _httpClient.PatchAsJsonAsync($"Task/{id}/priority", priority);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update priority for task {id}");
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Task/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete task {id} via API");
            return false;
        }
    }
}
