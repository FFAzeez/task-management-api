namespace TeamTaskManagementAPI.Domain.BindingModels.Requests;

public class UserLoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}