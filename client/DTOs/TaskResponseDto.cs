using System.ComponentModel.DataAnnotations;
using client.Enums;

namespace client.DTOs;

public class TaskResponseDto
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public PriorityTask Priority { get; set; }

    public StatusTask Status { get; set; } = StatusTask.Pending;

    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
