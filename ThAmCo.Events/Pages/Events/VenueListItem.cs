namespace ThAmCo.Events.Pages.Events
{
    public class VenueListItem
    {
        public string VenueName { get; set; } = string.Empty;
        public string VenueCode { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public double CostPerHour { get; set; }

    }
}