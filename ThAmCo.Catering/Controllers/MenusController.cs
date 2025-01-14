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
    public class MenusController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenusController(CateringDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all menus
        /// </summary>
        /// <returns></returns>
        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuGetDTO>>> GetMenus()
        {
            // Get all menus
            var menus = await _context.Menus.ToListAsync();
            // Create a DTO for each menu
            var menuDto = menus.Select(m => new MenuGetDTO
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName
            });
            // Return the DTOs
            return Ok(menuDto);

        }

        /// <summary>
        /// Get a menu by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuGetDTO>> GetMenu(int id)
        {
            // Find the menu by id
            var menu = await _context.Menus.FindAsync(id);
            // Create a DTO for the menu
            var menuDto = new MenuGetDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName
            };
            // If the menu does not exist, return a 400 Bad Request
            if (menu == null)
            {
                return BadRequest("The menu with the given id does not exist.");
            }
            // Return the DTO
            return Ok(menuDto);
        }
        /// <summary>
        /// Get food item details in a menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Details")]
        public async Task<ActionResult<ThAmCo.Catering.DTO.FoodItemsInMenuDTO>> GetFoodItemsInMenu(int id)
        {
            // Find the menu by id
            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .ThenInclude(mfi => mfi.FoodItem)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            // If the menu does not exist, return a 400 Bad Request
            if (menu == null)
            {
                return BadRequest("The menu with the given id does not exist.");
            }
            // Create a DTO object to return
            var foodItemsInMenu = new ThAmCo.Catering.DTO.FoodItemsInMenuDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                FoodItems = menu.MenuFoodItems.Select(mfi => new ThAmCo.Catering.DTO.FoodItemGetDTO
                {
                    FoodItemId = mfi.FoodItem.FoodItemId,
                    Description = mfi.FoodItem.Description,
                    UnitPrice = mfi.FoodItem.UnitPrice
                }).ToList()
            };
            // Return the DTO
            return foodItemsInMenu;
        }

        /// <summary>
        /// Edit a menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, ThAmCo.Catering.DTO.MenuPostDTO menu)
        {
            // Find the menu by id
            var thisMenu = await _context.Menus.FindAsync(id);
            // If the menu does not exist, return a 400 Bad Request
            if (thisMenu == null)
            {
                return BadRequest("The menu with the given id does not exist.");
            }

            // Update the properties of the entity directly
            thisMenu.MenuName = menu.MenuName;

            // Mark the entity as modified
            _context.Entry(thisMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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
        /// Add a menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        // POST: api/Menus
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(ThAmCo.Catering.DTO.MenuPostDTO menu)
        {
            // Create a new menu object
            Menu thisMenu = new Menu
            {
                MenuName = menu.MenuName,
                 FoodBookings= null,
                MenuFoodItems = null, 
            };
            // Add the menu to the context
            _context.Menus.Add(thisMenu);
            // Save the changes
            await _context.SaveChangesAsync();
            // Return the menu
            return CreatedAtAction("GetMenu", new { id = thisMenu.MenuId }, thisMenu);
        }

        /// <summary>
        /// Delete a menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            // Find the menu by id
            var menu = await _context.Menus.FindAsync(id);
            // If the menu does not exist, return a 400 Bad Request
            if (menu == null)
            {
                return BadRequest("The menu with the given id does not exist.");
            }
            // Remove the menu
            _context.Menus.Remove(menu);
            // Save the changes
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
