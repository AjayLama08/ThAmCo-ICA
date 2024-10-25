using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        [Required]
        public int StaffingId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int StaffId { get; set; }

        [Required]
        public string StaffRole { get; set; }

        //Navigation property with Event class
        public Event Event { get; set; }

        //Navigation property with Staff class
        public Staff Staff { get; set; }

    }
}
