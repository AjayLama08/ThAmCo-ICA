using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.EventStaffs
{
    public class RemoveModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public RemoveModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staffing Staffing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? eventId, int? staffId)
        {
            if (eventId == null || staffId == null)
            {
                return NotFound();
            }

            var staffing = await _context.Staffings
                .Include(s => s.Staff)
                .Include(s => s.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.StaffId == staffId);

            if (staffing == null)
            {
                return NotFound();
            }
            else
            {
                Staffing = staffing;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? eventId, int? staffId)
        {
            if (eventId == null || staffId == null)
            {
                return NotFound();
            }

            var staffing = await _context.Staffings.FirstOrDefaultAsync(m => m.EventId == eventId && m.StaffId == staffId);
            if (staffing != null)
            {
                Staffing = staffing;
                _context.Staffings.Remove(Staffing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}