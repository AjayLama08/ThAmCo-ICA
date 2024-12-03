using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.EventGuests
{
    public class RemoveModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public RemoveModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? eventId, int? guestId)
        {
            if (eventId == null || guestId == null)
            {
                return NotFound();
            }

            var guestbooking = await _context.GuestBookings
                .Include(g => g.Guest)
                .Include(g => g.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.GuestId == guestId);

            if (guestbooking == null)
            {
                return NotFound();
            }
            else
            {
                GuestBooking = guestbooking;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? eventId, int? guestId)
        {
            if (eventId == null || guestId == null)
            {
                return NotFound();
            }

            var guestbooking = await _context.GuestBookings.FirstOrDefaultAsync(m => m.EventId == eventId && m.GuestId == guestId);
            if (guestbooking != null)
            {
                GuestBooking = guestbooking;
                _context.GuestBookings.Remove(GuestBooking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Events/Details", new {id = eventId});
        }
    }
}
