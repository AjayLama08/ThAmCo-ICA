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
    public class DeleteModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public DeleteModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                Event = eventToDelete;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
