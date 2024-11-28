using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages
{
    public class ReserveVenueModel : PageModel
    {
        private readonly VenueService _venueService;

        public ReserveVenueModel(VenueService venueService)
        {
            _venueService = venueService;
        }

        [BindProperty]
        public string EventId { get; set; }

        [BindProperty]
        public string VenueId { get; set; }

        public List<VenueDTO> AvailableVenues { get; set; }

        public async Task<IActionResult> OnGetAsync(string eventId)
        {
            EventId = eventId;
            AvailableVenues = await _venueService.GetAvailableVenuesAsync(eventId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _venueService.ReserveVenueAsync(EventId, VenueId);
            if (success)
            {
                // Redirect to the event details page or show a success message
                return RedirectToPage("Details", new { id = EventId });
            }
            else
            {
                // Show an error message
                ModelState.AddModelError("", "Failed to reserve the venue.");
                return Page();
            }
        }
    }
}