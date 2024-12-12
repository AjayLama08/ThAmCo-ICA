using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Events.Pages.Guests
{
    public class AnonymiseModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public AnonymiseModel(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int GuestId { get; set; }
        [BindProperty]
        public ThAmCo.Events.Data.Guest guest { get; set; }

        public void OnGet()
        {

        }
        public static string Pseudonymise(string input)
        {
            return "Anonymised_" + input.GetHashCode();
        }

        public IActionResult OnPost()
        {
            if (guest == null)
            {
                // Handle the null guest scenario
                return BadRequest("Guest object is null");
            }

            // Check if the guest exists in the database
            var existingGuest = _context.Guests.Find(GuestId);

            while (existingGuest == null)
            {
                // Handle the scenario where the guest is not found
                ModelState.AddModelError("GuestId", "Guest not found");
                return Page();
            }
                // Pseudonymize the guest details
                existingGuest.FirstName = Pseudonymise(existingGuest.FirstName);
                existingGuest.LastName = Pseudonymise(existingGuest.LastName);
                existingGuest.Email = Pseudonymise(existingGuest.Email);

                // Update the guest in the database
                _context.Attach(existingGuest).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("./Index");
            }
        }
    }


