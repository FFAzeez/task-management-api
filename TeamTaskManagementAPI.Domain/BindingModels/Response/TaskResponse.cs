namespace TeamTaskManagementAPI.Domain.BindingModels.Response;

public class TaskResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; }
    public string TeamId { get; set; }
    public string CreatedByUserId { get; set; }
    public string? AssignedToUserId { get; set; }
}