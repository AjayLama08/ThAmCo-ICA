using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTO;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all food items
        /// </summary>
        /// <returns></returns>
        // GET: api/FoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItemGetDTO>>> GetFoodItems()
        {
            // Get all food items
            var foodItem = await _context.FoodItems.ToListAsync();
            // Create a DTO for each food item
            var foodItemDto = foodItem.Select(f => new FoodItemGetDTO
            {
                FoodItemId = f.FoodItemId,  
                Description = f.Description,
                UnitPrice = f.UnitPrice
            });
            // Return the DTOs
            return Ok(foodItemDto);
        }

        /// <summary>
        /// Get a food item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/FoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemGetDTO>> GetFoodItem(int id)
        {
            // Find the food item by id
            var foodItem = await _context.FoodItems.FindAsync(id);
            // If the food item does not exist, return a 400 Bad Request
            if (foodItem == null)
            {
                return BadRequest("Food item with the given id does not exist.");
            }
            // Create a DTO for the food item
            var foodItemDto = new FoodItemGetDTO
            {
                FoodItemId = foodItem.FoodItemId,
                Description = foodItem.Description,
                UnitPrice = foodItem.UnitPrice
            };
            // Return the DTO
            return foodItemDto;
        }

        /// <summary>
        ///  Edit a food item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        // PUT: api/FoodItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, ThAmCo.Catering.DTO.FoodItemPostDTO foodItem)
        {
            // Find the food item by id
            var thisFoodItem = await _context.FoodItems.FindAsync(id);
            // If the food item does not exist, return a 400 Bad Request
            if (thisFoodItem == null)
              {
                return BadRequest("Food item with the given id does not exist.");
              }
            

            // Update the properties of the entity directly
            thisFoodItem.Description = foodItem.Description;
            thisFoodItem.UnitPrice = foodItem.UnitPrice;

            // Mark the entity as modified
            _context.Entry(thisFoodItem).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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
        /// Add a food item
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        // POST: api/FoodItems
        [HttpPost]
        public async Task<ActionResult<FoodItem>> PostFoodItem(ThAmCo.Catering.DTO.FoodItemPostDTO foodItem)
        {
            // Create a new food item
            FoodItem thisFoodItem = new FoodItem
            {
                Description = foodItem.Description,
                UnitPrice = foodItem.UnitPrice
            };

            // Add the food item to the database
            _context.FoodItems.Add(thisFoodItem);
            // Save the changes
            await _context.SaveChangesAsync();
            // Return the newly created food item
            return CreatedAtAction("GetFoodItem", new { id = thisFoodItem.FoodItemId }, thisFoodItem);
        }

        /// <summary>
        /// Delete a food item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/FoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound("Food item with the given id does not exist.");
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemId == id);
        }
    }
}
