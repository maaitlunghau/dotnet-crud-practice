using System.ComponentModel.DataAnnotations;
using server.Enums;

namespace server.DTOs;

public class TaskUpdateDto
{
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public string? Title { get; set; }

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
    public string? Description { get; set; }

    public PriorityTask? Priority { get; set; }

    public StatusTask? Status { get; set; }

    public DateTime? DueDate { get; set; }
}
