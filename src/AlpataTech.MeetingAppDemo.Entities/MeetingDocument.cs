﻿using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingDocument : BaseEntitiy
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentPath { get; set; }
    }
}
