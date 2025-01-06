using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    [Authorize(Roles = "Manager,Team Leader")]
    public class DeleteModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly VenueService _venueService;

        public DeleteModel(ThAmCo.Events.Data.EventsDbContext context, VenueService venueService)
        {
            _context = context;
            _venueService = venueService;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get the event to delete
            var eventToDelete = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);

            if (eventToDelete == null)
            {
                return NotFound();
            }
            else
            {
                Event = eventToDelete;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get the event to delete
            var eventToDelete = await _context.Events.FindAsync(id);
            // Get all staffings for the event
            var staffingsToDelete = await _context.Staffings.Where(s => s.EventId == id).ToListAsync();
            if (eventToDelete != null)
            {
                // Soft delete the event and remove all staffings
                eventToDelete.IsDeleted = true;
                _context.Entry(eventToDelete).State = EntityState.Modified;
                _context.Staffings.RemoveRange(staffingsToDelete);

                // Free the venue
                await _venueService.FreeVenueAsync(eventToDelete.ReservationReference);

                // Save changes
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
