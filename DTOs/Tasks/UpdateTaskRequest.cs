using System.ComponentModel.DataAnnotations;
using TodoSaaS.Models;

namespace TodoSaaS.DTOs.Tasks;

public class UpdateTaskRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public TaskItemStatus Status { get; set; }
}
