using TeamTaskManagementAPI.Domain.Enums;

namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class UpdateTaskStatusRequest
{
    public TasksStatus Status { get; set; } 
    public string UserId { get; set; }
}