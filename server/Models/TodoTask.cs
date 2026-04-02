using System.ComponentModel.DataAnnotations;
using server.Enums;

namespace server.Models;

public class TodoTask
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
    public string Description { get; set; } = string.Empty;

    public PriorityTask Priority { get; set; }

    public StatusTask Status { get; set; } = StatusTask.Pending;

    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
