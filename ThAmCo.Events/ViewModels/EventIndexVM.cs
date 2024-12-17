namespace ThAmCo.Events.ViewModels
{
    public class EventIndexVM
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public string? ReservationReference { get; set; }
        public int GuestCount { get; set; }
        public string VenueCode { get; set; } = string.Empty;
    }
}
