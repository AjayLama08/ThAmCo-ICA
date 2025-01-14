using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTO;

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
        public async Task<ActionResult<IEnumerable<FoodBookingGetDTO>>> GetFoodBookings()
        {
            //Get all food bookings
            var foodBooking = await _context.FoodBookings.ToListAsync();
            //Create a DTO for each food booking
            var foodBookingDto = foodBooking.Select(f => new FoodBookingGetDTO
            {
                FoodBookingId = f.FoodBookingId,
                ClientReferenceId = f.ClientReferenceId,
                NumberOfGuests = f.NumberOfGuests,
                MenuId = f.MenuId,
            });
            //Return the DTOs
            return Ok(foodBookingDto);
        }

        /// <summary>
        /// Get a food booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/FoodBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodBookingGetDTO>> GetFoodBooking(int id)
        {
            //Find the food booking by id
            var foodBooking = await _context.FoodBookings.FindAsync(id);
            //If the food booking does not exist, return a 400 Bad Request
            if (foodBooking == null)
            {
                return BadRequest("The food booking with the given id does not exist.");
            }
            //Create a DTO for the food booking
            var foodBookingDto = new FoodBookingGetDTO
            {
                FoodBookingId = foodBooking.FoodBookingId,
                ClientReferenceId = foodBooking.ClientReferenceId,
                NumberOfGuests = foodBooking.NumberOfGuests,
                MenuId = foodBooking.MenuId,
            };
            //Return the DTO
            return foodBookingDto;
        }

        /// <summary>
        /// Edit a food booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodBooking"></param>
        /// <returns></returns>
        // PUT: api/FoodBookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodBooking(int id, FoodBookingPostDTO foodBooking )
        {
            //Get the food booking by id
            var thisFoodBooking = await _context.FoodBookings.FindAsync(id);
            //If the food booking does not exist, return a 400 Bad Request
            if (thisFoodBooking == null) {
                return BadRequest("The food booking with the given id does not exist.");
            }
            //Update the food booking
            thisFoodBooking.ClientReferenceId = foodBooking.ClientReferenceId;
            thisFoodBooking.NumberOfGuests = foodBooking.NumberOfGuests;
            thisFoodBooking.MenuId = foodBooking.MenuId;

            //Set the state of the food booking to modified
            _context.Entry(thisFoodBooking).State = EntityState.Modified;

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
        [HttpPost]
        public async Task<ActionResult<FoodBooking>> PostFoodBooking(FoodBookingPostDTO foodBooking)
        {
            //Create a new food booking
            FoodBooking thisFoodBooking = new FoodBooking
            {
                ClientReferenceId = foodBooking.ClientReferenceId,
                NumberOfGuests = foodBooking.NumberOfGuests,
                MenuId = foodBooking.MenuId,
            };
            //Add the food booking to the database
            _context.FoodBookings.Add(thisFoodBooking);
            //Save the changes
            await _context.SaveChangesAsync();
            //Return the food booking
            return CreatedAtAction("GetFoodBooking", new { id = thisFoodBooking.FoodBookingId }, foodBooking);
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
                return BadRequest("The food booking with the given id does not exist.");
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
