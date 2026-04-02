using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Enums;
using server.Models;
using server.Repositories;

namespace server.Services;

public class TaskService : ITaskRepository
{
    private readonly DataContext _dbContext;
    public TaskService(DataContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<TodoTask>> GetAllTasksAsync()
    {
        return await _dbContext.Tasks.AsNoTracking().ToListAsync();
    }

    public async Task<TodoTask?> GetTaskByIdAsync(Guid? id)
    {
        return await _dbContext.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TodoTask?> CreateTaskAsync(TodoTask? task)
    {
        if (task is null) return null;

        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task<TodoTask?> UpdateTaskAsync(TodoTask? task)
    {
        if (task is null) return null;

        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task<TodoTask?> UpdateTaskStatusAsync(Guid? id, StatusTask status)
    {
        var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask is null) return null;

        existingTask.Status = status;
        await _dbContext.SaveChangesAsync();

        return existingTask;
    }

    public async Task<TodoTask?> UpdateTaskPriorityAsync(Guid? id, PriorityTask priority)
    {
        var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask is null) return null;

        existingTask.Priority = priority;
        await _dbContext.SaveChangesAsync();

        return existingTask;
    }

    public async Task DeleteTaskAsync(Guid? id)
    {
        var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask is null) return;

        _dbContext.Tasks.Remove(existingTask);
        await _dbContext.SaveChangesAsync();
    }
}
