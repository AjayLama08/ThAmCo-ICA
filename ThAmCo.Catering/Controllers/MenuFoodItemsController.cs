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
    public class MenuFoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenuFoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all food items in a menu
        /// </summary>
        /// <returns></returns>
        // GET: api/MenuFoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuFoodItem>>> GetMenuFoodItems()
        {
            return await _context.MenuFoodItems.ToListAsync();
        }

        /// <summary>
        /// Get food items in a menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/MenuFoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuFoodItem>> GetMenuFoodItem(int id)
        {
            var menuFoodItem = await _context.MenuFoodItems.FindAsync(id);

            if (menuFoodItem == null)
            {
                return NotFound();
            }

            return menuFoodItem;
        }

        /// <summary>
        /// Edit a food item in a menu
        /// </summary>
        /// <param name="foodItemId"></param>
        /// <param name="menuId"></param>
        /// <param name="menuFoodItem"></param>
        /// <returns></returns>
        // PUT: api/MenuFoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{foodItemId}/{menuId}")]
        public async Task<IActionResult> PutMenuFoodItem(int foodItemId, int menuId, MenuFoodItem menuFoodItem)
        {
            // Check if the food item and menu id match
            if (foodItemId != menuFoodItem.FoodItemId || menuId != menuFoodItem.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(menuFoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuFoodItemExists(foodItemId, menuId))
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
        /// Add a food item to a menu
        /// </summary>
        /// <param name="menuFoodItem"></param>
        /// <returns></returns>
        // POST: api/MenuFoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuFoodItem>> PostMenuFoodItem(ThAmCo.Catering.DTO.MenuFoodItemDTO menuFoodItem)
        {
            // Create a new menu food item
            MenuFoodItem thisMenuFoodItem = new MenuFoodItem
            {
                MenuId = menuFoodItem.MenuId,
                FoodItemId = menuFoodItem.FoodItemId
            };

            _context.MenuFoodItems.Add(thisMenuFoodItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuFoodItemExists(menuFoodItem.MenuId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenuFoodItem", new { id = thisMenuFoodItem.MenuId }, thisMenuFoodItem);
        }

        private bool MenuFoodItemExists(int menuId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a food item from a menu 
        /// </summary>
        /// <param name="menuFoodItemDTO"></param>
        /// <returns></returns>
        // DELETE: api/MenuFoodItems/5
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuFoodItem(ThAmCo.Catering.DTO.MenuFoodItemDTO menuFoodItemDTO)
        {
            // Find the menu food item by food item id and menu id
            var menuFoodItem = await _context.MenuFoodItems
                .FirstOrDefaultAsync(mfi => mfi.FoodItemId == menuFoodItemDTO.FoodItemId && mfi.MenuId == menuFoodItemDTO.MenuId);

            if (menuFoodItem == null)
            {
                return NotFound();
            }

            _context.MenuFoodItems.Remove(menuFoodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuFoodItemExists(int foodItemId, int menuId)
        {
            return _context.MenuFoodItems.Any(e => e.FoodItemId == foodItemId && e.MenuId == menuId);
        }
    }
}

