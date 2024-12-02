using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Services;
using ThAmCo.Events.ViewModels;

namespace ThAmCo.Events.Pages.Events
{
    public class ReserveVenueModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly AvailabilityService _availabilityService;

        public List<SelectListItem> AvailableVenues { get; set; }

        [BindProperty]
        public ReserveVenueVM ReserveVenue { get; set; }

        public ReserveVenueModel(ThAmCo.Events.Data.EventsDbContext context, AvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
            AvailableVenues = new List<SelectListItem>();
        }

        public EventVM Event { get; set; }

        [BindProperty]
        public int EventId { get; set; }

        [BindProperty]
        public string VenueCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventToReserve = await _context.Events.Select(e => new EventVM
            {
                EventId = e.EventId,
                Title = e.Title,
                DateAndTime = e.DateAndTime,
                EventTypeId = e.EventTypeId,
                FoodBookingId = e.FoodBookingId,
                ReservationReference = e.ReservationReference
            }).FirstOrDefaultAsync(m => m.EventId == id);

            if (eventToReserve == null)
            {
                return NotFound();
            }

            Event = eventToReserve;
            ReserveVenue = new ReserveVenueVM();
            ReserveVenue.EventId = eventToReserve.EventId;
            EventId = eventToReserve.EventId;

            DateTime beginDate = eventToReserve.DateAndTime;
            DateTime endDate = eventToReserve.DateAndTime.AddDays(1); // Assuming endDate is one day after beginDate
            string eventType = eventToReserve.EventTypeId;

            var availableVenues = await _availabilityService.GetAvailabilityListAsync(beginDate, endDate, eventType);
            if (availableVenues != null)
            {
                AvailableVenues = availableVenues.Select(v => new SelectListItem
                {
                    Text = v.VenueCode,
                    Value = v.VenueCode
                }).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
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
            }
            return RedirectToPage("./Index");
        }

        private bool EventExists(int eventId)
        {
            return _context.Events.Any(e => e.EventId == eventId);
        }
    }
}