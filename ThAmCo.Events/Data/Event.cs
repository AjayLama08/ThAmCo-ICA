using System.ComponentModel.DataAnnotations;
using ThAmCo.Catering.Data;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Data
{
    public class Event
    {





        //Navigation property with Catering.Data.FoodBooking
        public FoodBooking FoodBooking { get; set; }
        //Navigation property with Venues.Data.Reservation
        public Reservation Reservation { get; set; }
        //Navigation property with Venues.Data.EventType
        public EventType EventType { get; set; }
    }
}
