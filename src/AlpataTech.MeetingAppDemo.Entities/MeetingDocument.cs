using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingDocument : BaseEntitiy
    {
        // Navigation property to the associated Meeting
        [Required]
        public int MeetingId { get; set; }
        [Required]
        public Meeting Meeting { get; set; }

        [Required]
        public String DocumentTitle { get; set; }


        [Required]
        public String DocumentPath { get; set; }
    }
}
