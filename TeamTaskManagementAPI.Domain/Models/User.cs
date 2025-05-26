namespace TeamTaskManagementAPI.Domain.Models;

public class User:BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime LastLoginDate { get; set; }
    public ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
    public ICollection<TaskModel> CreatedTasks { get; set; } = new List<TaskModel>();
    public ICollection<TaskModel> AssignedTasks { get; set; } = new List<TaskModel>();
}