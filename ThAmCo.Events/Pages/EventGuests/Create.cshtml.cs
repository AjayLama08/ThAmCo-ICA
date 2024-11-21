using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult OnGet()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
            ViewData["GuestId"] = new SelectList(_context.Guests.Select(g => new { g.GuestId, FullName = g.FirstName + " " + g.LastName }), "GuestId", "FullName");
            return Page();
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GuestBookings.Add(GuestBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
