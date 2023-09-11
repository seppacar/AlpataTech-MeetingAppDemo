﻿using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingParticipant : BaseEntitiy
    {
        // Navigation property to the associated Meeting
        [Required]
        public int MeetingId { get; set; }
        [Required]
        public Meeting Meeting { get; set; }

        // Navigation property to the associated User
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
