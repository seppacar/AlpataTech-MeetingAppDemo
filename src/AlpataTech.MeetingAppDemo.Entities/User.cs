﻿using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class User : BaseEntitiy
    {
        [Required]
        public String FirstName { get; set; } = String.Empty;

        [Required]
        public String LastName { get; set; } = String.Empty;

        [Required]
        public string Email { get; set; } = String.Empty;

        public String? PhoneNumber { get; set; }
        
        [Required]
        public String PasswordHash { get; set; } = String.Empty;

        [Required]
        public String ProfileImage { get; set; } = String.Empty;
}