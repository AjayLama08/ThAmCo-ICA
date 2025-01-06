using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodBookingsController(CateringDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all food bookings
        /// </summary>
        /// <returns></returns>
        // GET: api/FoodBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodBooking>>> GetFoodBookings()
        {
            return await _context.FoodBookings.ToListAsync();
        }

        /// <summary>
        /// Get a food booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/FoodBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodBooking>> GetFoodBooking(int id)
        {
            //Find the food booking by id
            var foodBooking = await _context.FoodBookings.FindAsync(id);

            if (foodBooking == null)
            {
                return NotFound();
            }

            return foodBooking;
        }

        /// <summary>
        /// Edit a food booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodBooking"></param>
        /// <returns></returns>
        // PUT: api/FoodBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodBooking(int id, FoodBooking foodBooking)
        {
            //Check if the id is the same as the food booking id
            if (id != foodBooking.FoodBookingId)
            {
                return BadRequest();
            }

            //Set the state of the food booking to modified
            _context.Entry(foodBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add a food booking
        /// </summary>
        /// <param name="foodBooking"></param>
        /// <returns></returns>
        // POST: api/FoodBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodBooking>> PostFoodBooking(FoodBooking foodBooking)
        {
            _context.FoodBookings.Add(foodBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodBooking", new { id = foodBooking.FoodBookingId }, foodBooking);
        }

        /// <summary>
        /// Delete a food booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/FoodBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodBooking(int id)
        {
            //Find the food booking by id
            var foodBooking = await _context.FoodBookings.FindAsync(id);
            if (foodBooking == null)
            {
                return NotFound();
            }
            //Remove the food booking
            _context.FoodBookings.Remove(foodBooking);
            //Save the changes
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodBookingExists(int id)
        {
            return _context.FoodBookings.Any(e => e.FoodBookingId == id);
        }
    }
}
