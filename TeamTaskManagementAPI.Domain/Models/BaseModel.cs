

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamTaskManagementAPI.Domain.Models
{
    public class BaseModel
    {
        [Key]
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
