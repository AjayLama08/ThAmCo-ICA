using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Dtos;
using ThAmCo.Events.Services;
using ThAmCo.Events.ViewModels;

namespace ThAmCo.Events.Pages.Events
{
    public class ReserveVenueModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly ReservationService _reservationService;

        public List<SelectListItem> AvailableVenues { get; set; }

        [BindProperty]
        public ReservationPostVM Reservation { get; set; }

        public ReserveVenueModel(ThAmCo.Events.Data.EventsDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
            AvailableVenues = [];
        }
        public ReservationGetVM Availability { get; set; } = default!;

        [BindProperty]
        public int EventId { get; set; }

        [BindProperty]
        public int VenueCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventToReserve = await _context.Events.
                Select(e => new ReservationGetVM
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    DateAndTime = e.DateAndTime,
                    ReservationReference = e.ReservationReference
                })
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (eventToReserve == null)
            {
                return NotFound();
            }

            Availability = eventToReserve;

            if(Reservation == null)
            {
                Reservation = new ReservationPostVM();
            }

            Reservation.EventId = Availability.EventId;
            EventId = eventToReserve.EventId;

            var availableVenues = await _reservationService.GetVenueAvailabilityListAsync(Availability.DateAndTime, Availability.EventTypeId);

            if (availableVenues != null)
            {
                AvailableVenues = availableVenues.Select(a => new SelectListItem
                {
                    Value = a.VenueCode.ToString(),
                    Text = a.Reference
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
            //_context.Attach(Workshop).State = EntityState.Modified; // Remove for now
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(EventId))
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

        private bool AvailabilityExists(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
