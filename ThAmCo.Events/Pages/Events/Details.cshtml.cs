using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
            Guests = new List<ThAmCo.Events.Data.Guest>();
            GuestBookings = new List<GuestBooking>();
        }

        public Event Event { get; set; } = default!;
        public List<ThAmCo.Events.Data.Guest> Guests { get; set; }
        public List<GuestBooking> GuestBookings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventForDetails = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (eventForDetails == null)
            {
                return NotFound();
            }
            else
            {
                Event = eventForDetails;
                var eventGuests = await _context.Guests
                    .Include(gb => gb.GuestBookings)
                    .ThenInclude(e => e.Event)
                    .Where(gb => gb.GuestBookings.Any(e => e.EventId == id))
                    .ToListAsync();

                if (eventGuests != null)
                {
                    Guests = eventGuests;
                }
                return Page();
            }
        }
        public async Task<IActionResult> OnPostRegisterAttendanceAsync(int eventId, int guestId)
        {
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

        public async Task<IActionResult> OnPostUnregisterAttendanceAsync(int eventId, int guestId)
        {
            var guestBooking = await _context.GuestBookings
                .FirstOrDefaultAsync(gb => gb.EventId == eventId && gb.GuestId == guestId);

            if (guestBooking == null)
            {
                return NotFound();
            }

            guestBooking.HasAttended = false;
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = eventId });
        }
    }
}
