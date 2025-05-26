using TeamTaskManagementAPI.Domain.Enums;

namespace TeamTaskManagementAPI.Domain.Models;

public class TaskModel: BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TasksStatus Status { get; set; }
    public string TeamId { get; set; }
    public virtual Team Team { get; set; }
    public string CreatedByUserId { get; set; }
    public virtual User CreatedBy { get; set; }
    public string? AssignedToUserId { get; set; }
    public virtual User AssignedTo { get; set; }
}