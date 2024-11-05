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
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItems()
        {
            return await _context.FoodItems.ToListAsync();
        }

        /// <summary>
        /// Get a food item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/FoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItem>> GetFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        /// <summary>
        ///  Edit a food item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, ThAmCo.Catering.DTO.FoodItemDTO foodItem)
        {
            var thisFoodItem = await _context.FoodItems.FindAsync(id);

            if (thisFoodItem == null)
            {
                return BadRequest();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodItem>> PostFoodItem(ThAmCo.Catering.DTO.FoodItemDTO foodItem)
        {
            FoodItem thisFoodItem = new FoodItem
            {
                Description = foodItem.Description,
                UnitPrice = foodItem.UnitPrice
            };

            _context.FoodItems.Add(thisFoodItem);
            await _context.SaveChangesAsync();

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
                return NotFound();
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
