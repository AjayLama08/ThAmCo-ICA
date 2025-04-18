﻿using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.ViewModels
{
    public class EventVM
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public string EventTypeId { get; set; } = string.Empty;
        public int? FoodBookingId { get; set; }
        public string? ReservationReference { get; set; }

    }
}
