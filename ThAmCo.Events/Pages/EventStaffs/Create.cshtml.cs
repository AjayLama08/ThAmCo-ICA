using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.EventStaffs
{
    [Authorize(Roles = "Manager,Team Leader")]
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public CreateModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? eventId)
        {
            Staffing = new Staffing
            {
                EventId = eventId ?? 0
            };
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
            ViewData["StaffId"] = new SelectList(_context.Staffs.Select(s => new { s.StaffId, FullName = s.FirstName + " " + s.LastName }), "StaffId", "FullName");
            return Page();
        }

        [BindProperty]
        public Staffing Staffing { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the staff is already assigned to the event
            var existingStaffing = _context.Staffings
                .FirstOrDefault(s => s.StaffId == Staffing.StaffId && s.EventId == Staffing.EventId);

            if (existingStaffing != null)
            {
                ModelState.AddModelError(string.Empty, "This staff is already assigned to the event.");
                ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
                ViewData["StaffId"] = new SelectList(_context.Staffs.Select(s => new { s.StaffId, FullName = s.FirstName + " " + s.LastName }), "StaffId", "FullName");
                return Page();
            }

            _context.Staffings.Add(Staffing);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Details" , new { id = Staffing.EventId });
        }
    }
}
