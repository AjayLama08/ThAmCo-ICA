using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.Staffs
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
            Staffings = new List<Staffing>();
            Events = new List<Event>();
            int eventCount;
        }

        public Staff Staff { get; set; } = default!;
        public List<Staffing> Staffings { get; set; }
        public List<Event> Events { get; set; }
        public int eventCount { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Staffings)
                .ThenInclude(e => e.Event)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }
            else
            {
                Staff = staff;
                Staffings = staff.Staffings;
                Events = staff.Staffings.Select(e => e.Event).ToList();
                eventCount = Events.Count;

                return Page();
            }
        }
    }
}
