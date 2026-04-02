using System.ComponentModel.DataAnnotations;
using client.Enums;

namespace client.DTOs;

public class TaskCreateDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
    public string? Description { get; set; }

    public PriorityTask Priority { get; set; } = PriorityTask.Medium;

    public StatusTask Status { get; set; } = StatusTask.Pending;

    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }
}
