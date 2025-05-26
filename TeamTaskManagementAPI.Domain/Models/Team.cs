namespace TeamTaskManagementAPI.Domain.Models;

public class Team:BaseModel
{
    public string Name { get; set; }
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
    public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}