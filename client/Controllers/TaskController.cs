using client.DTOs;
using client.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace client.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return View(tasks);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TaskCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskCreateDto createDto)
    {
        if (!ModelState.IsValid) return View(createDto);

        var success = await _taskRepository.CreateTaskAsync(createDto);
        if (success) return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Đã có lỗi xảy ra khi tạo Task.");
        return View(createDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        if (existingTask == null) return NotFound();

        var updateDto = new TaskUpdateDto
        {
            Title = existingTask.Title,
            Description = existingTask.Description,
            Priority = existingTask.Priority,
            Status = existingTask.Status,
            DueDate = existingTask.DueDate
        };

        return View(updateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TaskUpdateDto updateDto)
    {
        if (!ModelState.IsValid) return View(updateDto);

        var success = await _taskRepository.UpdateTaskAsync(id, updateDto);
        if (success) return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Đã có lỗi xảy ra khi cập nhật Task.");
        return View(updateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _taskRepository.DeleteTaskAsync(id);
        if (!success) TempData["Error"] = "Không thể xóa Task này.";

        return RedirectToAction(nameof(Index));
    }
}
