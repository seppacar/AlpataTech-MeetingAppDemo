﻿using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities.DTO.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string ProfileImage { get; set; }
    }
}
