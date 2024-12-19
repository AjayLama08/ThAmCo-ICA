using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ThAmCo.Venues.Data;

namespace ThAmCo.Venues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : Controller
    {
        private readonly VenuesDbContext _context;

        public VenuesController(VenuesDbContext context)
        {
            _context = context;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetVenueDetails(string code)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.Code == code);

            if (venue == null)
            {
                return NotFound();
            }

            return Ok(venue);
        }
    }
}