namespace TeamTaskManagementAPI.Domain.BindingModels.Response;

public class UserResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}