namespace TeamTaskManagementAPI.Domain.Models;

public class TeamUser
{
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public string TeamId { get; set; }
    public virtual Team Team { get; set; }
    public string Role { get; set; } // Admin, Member
}