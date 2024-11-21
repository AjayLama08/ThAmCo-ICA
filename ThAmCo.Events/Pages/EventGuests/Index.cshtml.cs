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
    public class IndexModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public IndexModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public IList<GuestBooking> GuestBooking { get; set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            var eventsContext = _context.GuestBookings.AsQueryable();

            if (id != null)
            {
                eventsContext = eventsContext.Where(e => e.EventId == id);
            }
            eventsContext = eventsContext
                .Include(g => g.Event)
                .Include(g => g.Guest);

            GuestBooking = await eventsContext.ToListAsync();
        }
    }
}
