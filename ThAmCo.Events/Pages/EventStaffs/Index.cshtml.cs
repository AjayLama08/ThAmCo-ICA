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
    public class IndexModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public IndexModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public IList<Staffing> Staffing { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            var eventStaffsContext = _context.Staffings.AsQueryable();

            if (id != null)
            {
                eventStaffsContext = eventStaffsContext.Where(e => e.EventId == id);
            }
            eventStaffsContext = eventStaffsContext
                .Include(es => es.Event)
                .Include(es => es.Staff);

            Staffing = await eventStaffsContext.ToListAsync();
        }
    }
}
