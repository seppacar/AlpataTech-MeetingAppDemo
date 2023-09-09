using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class User : BaseEntitiy
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }

    }
}