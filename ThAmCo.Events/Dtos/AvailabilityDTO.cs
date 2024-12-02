using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events
{
    public class AvailabilityDTO
    {
        public DateTime Date { get; set; }
        public string VenueCode { get; set; } = string.Empty;
        public double CostPerHour { get; set; }
    }
}
