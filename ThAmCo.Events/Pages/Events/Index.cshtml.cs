using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Dtos;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{

    public class IndexModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly ThAmCo.Events.Services.VenueService _venueService;

        public IndexModel(ThAmCo.Events.Data.EventsDbContext context, VenueService venueService)
        {
            _context = context;
            _venueService = venueService ;
        }

        public IList<ThAmCo.Events.ViewModels.EventIndexVM> Event { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var events = await _context.Events.ToListAsync();
            Event = new List<ThAmCo.Events.ViewModels.EventIndexVM>();

            foreach (var e in events)
            {
                var venueInfo = e.ReservationReference != null
                    ? await _venueService.GetVenueCodeAsync(e.ReservationReference)
                    : "Not Known";

                var eventVM = new ThAmCo.Events.ViewModels.EventIndexVM
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    DateAndTime = e.DateAndTime,
                    ReservationReference = e.ReservationReference ?? "Not Known",
                    GuestCount = ThAmCo.Events.Data.GuestBooking.GetGuestCount(_context, e.EventId),
                    VenueCode = venueInfo
                };
                Event.Add(eventVM);
            }
        }
    }
}
