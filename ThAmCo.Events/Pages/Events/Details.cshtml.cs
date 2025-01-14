using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Dtos;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly VenueService _venueService;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, VenueService venueService)
        {
            _context = context;
            _venueService = venueService ;
            Guests = new List<ThAmCo.Events.Data.Guest>();
            Staffs = new List<ThAmCo.Events.Data.Staff>();
        }

        public Event Event { get; set; } = default!;
        public List<ThAmCo.Events.Data.Guest> Guests { get; set; }
        public List<GuestBooking> GuestBookings { get; set; }
        public List<ThAmCo.Events.Data.Staff> Staffs { get; set; }
        public string IsFirstAider { get; set; }
        public int GuestsCount { get; set; }
        public int StaffsCount { get; set; }

        public IList<ThAmCo.Events.ViewModels.VenueDetailsVM> Venue { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get the event to show details
            var eventForDetails = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (eventForDetails == null)
            {
                return NotFound();
            }
            else
            {
                Event = eventForDetails;
                // Get all the guest bookings for the event
                var eventGuests = await _context.Guests
                    .Include(gb => gb.GuestBookings)
                    .ThenInclude(e => e.Event)
                    .Where(gb => gb.GuestBookings.Any(e => e.EventId == id))
                    .ToListAsync();

                // Get all the staff for the event
                var eventStaffs = await _context.Staffs
                    .Include(s => s.Staffings)
                    .ThenInclude(e => e.Event)
                    .Where(s => s.Staffings.Any(e => e.EventId == id))
                    .ToListAsync();

                
                foreach (var staff in eventStaffs)
                {
                    if (staff.IsFirstAider == false)
                    {
                        IsFirstAider = "⚠️ Warning: No First-Aider for this event!";
                    }
                    else
                    {
                        break;
                    }
                }

                if (eventGuests != null)
                {
                    Guests = eventGuests;
                    GuestsCount = eventGuests.Count;
                }

                if (eventStaffs != null)
                {
                    Staffs = eventStaffs;
                    StaffsCount = eventStaffs.Count;
                }

                // Get the venue details
                var venueCode = Event.ReservationReference != null ? await _venueService.GetVenueCodeAsync(Event.ReservationReference) : "Not Known";

                var venueInfo = await _venueService.GetVenueDetailsAsync(venueCode);

                Venue = new List<ThAmCo.Events.ViewModels.VenueDetailsVM>
                 {
                   new ThAmCo.Events.ViewModels.VenueDetailsVM
                    {
                        VenueCode = venueInfo.VenueCode,
                        VenueName = venueInfo.VenueName,
                        Description = venueInfo.Description,
                        Capacity = venueInfo.Capacity,
                    }   
                 };
               return Page();
           }
      }

        public async Task<IActionResult> OnPostRegisterAttendanceAsync(int eventId, int guestId)
        {
            // Get the guest booking
            var guestBooking = await _context.GuestBookings
                .FirstOrDefaultAsync(gb => gb.EventId == eventId && gb.GuestId == guestId);

            if (guestBooking == null)
            {
                return NotFound();
            }

            guestBooking.HasAttended = true;
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = eventId });
        }
    }
}
