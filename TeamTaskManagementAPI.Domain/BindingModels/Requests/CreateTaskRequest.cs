namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class CreateTaskRequest
{
    public string UserId { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AssignedToUserId { get; set; }
    public DateTime? DueDate { get; set; }
}