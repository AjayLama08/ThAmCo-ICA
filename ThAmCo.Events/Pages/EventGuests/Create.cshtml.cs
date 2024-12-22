using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.EventGuests
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public CreateModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;


        public IActionResult OnGet(int? eventId)
        {
            GuestBooking = new GuestBooking
            {
                EventId = eventId ?? 0
            };
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
            ViewData["GuestId"] = new SelectList(_context.Guests.Select(g => new { g.GuestId, FullName = g.FirstName + " " + g.LastName }), "GuestId", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (ModelState.IsValid)
            //{
            //    return Page();
            //}

            var existingGuest = await _context.GuestBookings.FirstOrDefaultAsync(gb => gb.GuestId == GuestBooking.GuestId && gb.EventId == GuestBooking.EventId);
            if(existingGuest != null)
            {
                ModelState.AddModelError(string.Empty, "This guest is already booked in for the selected event.");
                ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
                ViewData["GuestId"] = new SelectList(_context.Guests.Select(g => new { g.GuestId, FullName = g.FirstName + " " + g.LastName }), "GuestId", "FullName");
                return Page();
            }

            _context.GuestBookings.Add(GuestBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Details", new { id = GuestBooking.EventId });
        }
    }
}
