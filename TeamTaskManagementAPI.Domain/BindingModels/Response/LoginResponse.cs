namespace TeamTaskManagementAPI.Domain.BindingModels.Response;

public class LoginResponse
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime LoginDate { get; set; }
}