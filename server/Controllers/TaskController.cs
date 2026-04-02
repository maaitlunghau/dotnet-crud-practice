using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Enums;
using server.Models;
using server.Repositories;
using System.Linq;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository)
            => _taskRepository = taskRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskEntities = await _taskRepository.GetAllTasksAsync();
            if (taskEntities == null || !taskEntities.Any())
                return NotFound();

            var reponseTasks = taskEntities.Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            });

            return Ok(reponseTasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            var taskEntity = await _taskRepository.GetTaskByIdAsync(id);
            if (taskEntity == null)
                return NotFound();

            var responseTask = new TaskResponseDto
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                Description = taskEntity.Description,
                Priority = taskEntity.Priority,
                Status = taskEntity.Status,
                DueDate = taskEntity.DueDate,
                CreatedAt = taskEntity.CreatedAt
            };

            return Ok(responseTask);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskEntity = new TodoTask
            {
                Title = createDto.Title,
                Description = createDto.Description ?? string.Empty,
                Priority = createDto.Priority,
                Status = createDto.Status,
                DueDate = createDto.DueDate
            };

            var createdTask = await _taskRepository.CreateTaskAsync(taskEntity);

            var responseTask = new TaskResponseDto
            {
                Id = createdTask!.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                Priority = createdTask.Priority,
                Status = createdTask.Status,
                DueDate = createdTask.DueDate,
                CreatedAt = createdTask.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = responseTask.Id },
                responseTask
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid? id, [FromBody] TaskUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null)
                return NotFound($"Task with ID {id} not found.");

            if (!string.IsNullOrEmpty(updateDto.Title))
                existingTask.Title = updateDto.Title;

            if (updateDto.Description != null)
                existingTask.Description = updateDto.Description;

            if (updateDto.Priority.HasValue)
                existingTask.Priority = updateDto.Priority.Value;

            if (updateDto.Status.HasValue)
                existingTask.Status = updateDto.Status.Value;

            if (updateDto.DueDate.HasValue)
                existingTask.DueDate = updateDto.DueDate.Value;

            await _taskRepository.UpdateTaskAsync(existingTask);

            return Ok(new TaskResponseDto
            {
                Id = existingTask.Id,
                Title = existingTask.Title,
                Description = existingTask.Description,
                Priority = existingTask.Priority,
                Status = existingTask.Status,
                DueDate = existingTask.DueDate,
                CreatedAt = existingTask.CreatedAt
            });
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid? id, [FromBody] StatusTask status)
        {
            var exisitingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (exisitingTask is null)
                return NotFound($"Task with ID {id} not found.");

            var updatedTask = await _taskRepository.UpdateTaskStatusAsync(id, status);

            return Ok(new TaskResponseDto
            {
                Id = updatedTask!.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                Priority = updatedTask.Priority,
                Status = updatedTask.Status,
                DueDate = updatedTask.DueDate,
                CreatedAt = updatedTask.CreatedAt
            });
        }

        [HttpPatch("{id:guid}/priority")]
        public async Task<IActionResult> UpdatePriority(Guid? id, [FromBody] PriorityTask priority)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask is null)
                return NotFound($"Task with ID {id} not found.");

            var updatedTask = await _taskRepository.UpdateTaskPriorityAsync(id, priority);

            return Ok(new TaskResponseDto
            {
                Id = updatedTask!.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                Priority = updatedTask.Priority,
                Status = updatedTask.Status,
                DueDate = updatedTask.DueDate,
                CreatedAt = updatedTask.CreatedAt
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask is null)
                return NotFound($"Task with ID {id} not found.");

            await _taskRepository.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}
