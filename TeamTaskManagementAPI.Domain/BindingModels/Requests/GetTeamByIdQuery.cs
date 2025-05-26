namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class GetTeamByIdQuery
{
    public string TaskId { get; set; }
    public string UserId { get; set; }
}