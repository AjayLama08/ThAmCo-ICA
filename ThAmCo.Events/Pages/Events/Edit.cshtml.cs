using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        private readonly EventTypeService _eventTypeService;

        // Empty SelectListItem List for category drop down
        public List<SelectListItem> EventTypeItems { get; set; } = [];
        public EditModel(ThAmCo.Events.Data.EventsDbContext context, EventTypeService eventTypeService)
        {
            _context = context;
            _eventTypeService = eventTypeService;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventToEdit =  await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (eventToEdit == null)
            {
                return NotFound();
            }
            Event = eventToEdit;

            //Popualte event type drop down using service class
            EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //Popualte event type drop down using service class to redisplay input form
                EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync();
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EventId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
