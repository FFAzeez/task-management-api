namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class UpdateTaskRequest
{
    public string TeamId { get; set; }
    public string UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedToUserId { get; set; }
}