using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities.Common
{
    public class BaseEntitiy
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
    }
}
