using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        private readonly EventTypeService _eventTypeService;

        // Empty SelectListItem List for event type drop down
        public List<SelectListItem> EventTypeItems { get; set; } = [];

        public CreateModel(ThAmCo.Events.Data.EventsDbContext context, EventTypeService eventTypeService)
        {
            _context = context;
            _eventTypeService = eventTypeService;
        }

        public async Task<IActionResult> OnGet()
        {
            //Popualte event type drop down using service class
            EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync();
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //Popualte event type drop down using service class to redisplay input form
                EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync();
                return Page();
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
