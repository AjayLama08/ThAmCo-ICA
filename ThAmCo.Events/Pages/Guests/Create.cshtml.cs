using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.Guest
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public CreateModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ThAmCo.Events.Data.Guest Guest { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            var existingGuest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Email == Guest.Email);

            if (existingGuest != null)
            {
                ModelState.AddModelError("Guest.Email", "The guest with this email already exists. Please try a with a different email.");
                return Page();
            }

            _context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
