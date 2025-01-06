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
            _venueService = venueService;
        }

        public List<ThAmCo.Events.ViewModels.EventIndexVM> Event { get; set; } = default!;
        public int StaffCount { get; set; }
        public int GuestCount { get; set; }
        public string HasSufficientStaff { get; set; }


        public async Task OnGetAsync()
        {
            var events = await _context.Events.Include(e => e.Staffings).ToListAsync();
            Event = new List<ThAmCo.Events.ViewModels.EventIndexVM>();

            bool allStaffSufficient = true; // Assume all are sufficient initially

            foreach (var e in events)
            {
                // Get the venue code
                var venueInfo = e.ReservationReference != null
                    ? await _venueService.GetVenueCodeAsync(e.ReservationReference)
                    : "Not Known";

                GuestCount = ThAmCo.Events.Data.GuestBooking.GetGuestCount(_context, e.EventId);
                StaffCount = ThAmCo.Events.Data.Staffing.GetStaffCount(_context, e.EventId);

                var isSufficient = StaffPerGuest(StaffCount, GuestCount);

                if (!isSufficient)
                {
                    allStaffSufficient = false; // Flag insufficient staffing
                }

                var eventVM = new ThAmCo.Events.ViewModels.EventIndexVM
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    DateAndTime = e.DateAndTime,
                    ReservationReference = e.ReservationReference ?? "Not Known",
                    GuestCount = GuestCount,
                    VenueCode = venueInfo,
                    IsStaffingSufficient = isSufficient
                };

                Event.Add(eventVM);
            }

            // Set HasSufficientStaff after checking all events
            HasSufficientStaff = allStaffSufficient ? "Yes" : "⚠️ Warning: No";
        }


        public bool StaffPerGuest(int staffCount, int guestCount)
        {
            return staffCount >= (guestCount / 10.0);
        }
    }
}


