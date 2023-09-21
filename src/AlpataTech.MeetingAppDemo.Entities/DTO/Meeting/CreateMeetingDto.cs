using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlpataTech.MeetingAppDemo.Entities.DTO.Meeting
{
    public class CreateMeetingDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [JsonIgnore]
        public int OrganizerId { get; set; }
    }
}
