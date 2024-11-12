using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staffing
    {

        public Staffing()
        {
        }

        public Staffing(int staffId, int eventId)
        {
            StaffId = staffId;
            EventId = eventId;
        }
        public int StaffId { get; set; }

        [Required]
        public int EventId { get; set; }

        //Navigation property with Event class
        public Event? Event { get; set; }

        //Navigation property with Staff class
        public Staff? Staff { get; set; }

    }
}
