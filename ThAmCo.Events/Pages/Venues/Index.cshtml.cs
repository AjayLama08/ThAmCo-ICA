using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.Events;
using ThAmCo.Events.Services;
using ThAmCo.Events.ViewModels;

namespace ThAmCo.Events.Pages.Venues
{
    public class IndexModel : PageModel
    {
        private readonly EventsDbContext _context;
        private readonly EventTypeService _eventTypeService;
        private readonly AvailabilityService _availabilityService;
        private readonly VenueService _venueService;

        public IndexModel(EventsDbContext context,EventTypeService eventTypeService, AvailabilityService availabilityService, VenueService venueService)
        {
            _context = context;
            _eventTypeService = eventTypeService;
            _availabilityService = availabilityService;
            _venueService = venueService;
        }

        // Empty SelectListItem List for event type drop down
        public List<SelectListItem> EventTypeItems { get; set; } = [];

        [BindProperty]
        public Event Event { get; set; } = default!;

        [BindProperty]
        public List<VenueListItem> AvailableVenues { get; set; }

        [BindProperty]
        public string EventType { get; set; }

        [BindProperty]
        public DateTime BeginDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public DateTime SelectedVenueDate { get; set; }

        [BindProperty]
        public string EventTitle { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BeginDate = new DateTime(2022, 11, 1);
            EndDate = BeginDate.AddMonths(4);
            EventType = "WED";

            //Popualte event type drop down using service class
            EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync(); ;

            var availableVenues = await _availabilityService.GetAvailabilityListAsync(BeginDate, EndDate, EventType);
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

        public async Task<IActionResult> OnPostFilterAsync()
        {
            // Fetch the event type select list
            EventTypeItems = await _eventTypeService.GetEventTypeSelectListAsync();

            // Filter available venues based on the selected event type
            var availableVenues = await _availabilityService.GetAvailabilityListAsync(BeginDate, EndDate, EventType);

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

        public async Task<IActionResult> OnPostCreateEventAsync()
        {
            BeginDate = new DateTime(2022, 1, 1);
            EndDate = DateTime.Now.AddMonths(12);
            EventType = "WED";

            var availableVenues = await _availabilityService.GetAvailabilityListAsync(BeginDate, EndDate, EventType);
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

            var selectedVenue = AvailableVenues.FirstOrDefault(v => v.Date == SelectedVenueDate);
            if (selectedVenue != null)
            {
                var newEvent = new Event
                {
                    EventTypeId = EventType,
                    Title = EventTitle,
                    DateAndTime = SelectedVenueDate, // Set the event date to the selected venue date
                    ReservationReference = $"{selectedVenue.VenueCode}{SelectedVenueDate:yyyyMMdd}"
                };

                _context.Events.Add(newEvent);

                var reserve = await _venueService.PostReserveVenue(selectedVenue.VenueCode, SelectedVenueDate);

                if (reserve == null)
                {
                    return BadRequest("Unable to reserve venue.");
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Events/Index");
        }

    }
}