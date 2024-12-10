using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Services;
using ThAmCo.Events.ViewModels;
using ThAmCo.Events.Dtos;

namespace ThAmCo.Events.Pages.Events
{
    public class ReserveVenueModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly AvailabilityService _availabilityService;

        public List<VenueListItem> AvailableVenues { get; set; }

        [BindProperty]
        public ReserveVenueVM ReserveVenue { get; set; }

        public ReserveVenueModel(ThAmCo.Events.Data.EventsDbContext context, AvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
            AvailableVenues = new List<VenueListItem>();
        }

        public EventVM Event { get; set; }

        [BindProperty]
        public int EventId { get; set; }

        [BindProperty]
        public string VenueCode { get; set; }

        [BindProperty]
        public DateTime EventDate { get; set; }

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

            /// this code snippet is for demo only - the deployed verion would need a credible collection of venue slots to select from.
            DateTime beginDate = eventToReserve.DateAndTime;
            DateTime endDate = eventToReserve.DateAndTime.AddMonths(6);
            string eventType = eventToReserve.EventTypeId;

            var availableVenues = await _availabilityService.GetAvailabilityListAsync(beginDate, endDate, eventType);
            if (availableVenues != null)
            {
                AvailableVenues = availableVenues.Select(v => new VenueListItem
                {
                    VenueName = v.venueName,
                    VenueCode = v.venueCode,
                    Date = v.date,
                    Capacity = v.capacity,
                    CostPerHour = v.costPerHour
                }).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostReserveVenueAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var eventToUpdate = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == EventId);

            if (eventToUpdate == null)
            {
                return NotFound();
            }
            
            var reserve = await _availabilityService.PostReserveVenue(VenueCode, EventDate);


            if (reserve == null)
            {
                return BadRequest("Unable to reserve venue.");
            }

            _context.Attach(eventToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                eventToUpdate.ReservationReference = $"{VenueCode}{EventDate.Date:yyyyMMdd}";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(EventId))
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

        private bool EventExists(int eventId)
        {
            return _context.Events.Any(e => e.EventId == eventId);
        }
    }
}