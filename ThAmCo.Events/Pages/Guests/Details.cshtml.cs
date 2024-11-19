using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.Guest
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
            Events = new List<Event>();
            GuestBookings = new List<GuestBooking>();
        }

        public ThAmCo.Events.Data.Guest Guest { get; set; } = default!;
        public List<GuestBooking> GuestBookings { get; set; }
        public List<Event> Events { get; set; }
       
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == id);

            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                Guest = guest;

                var eventNames = await _context.Events
                    .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                    .Where(e => e.GuestBookings.Any(gb => gb.GuestId == id))
                    .ToListAsync();

                if (eventNames != null)
                {
                    Events = eventNames;
                }
                return Page();
            }
        }
    }
}

