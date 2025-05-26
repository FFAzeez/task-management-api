namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class AddUserToTeamRequest
{
    public string? Role { get; set; }
    public string UserId { get; set; }
}