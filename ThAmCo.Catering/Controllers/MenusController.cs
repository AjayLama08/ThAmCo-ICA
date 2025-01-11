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
            var menus = await _context.Menus.ToListAsync();

            var menuDto = menus.Select(m => new MenuGetDTO
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName
            });
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
            var menu = await _context.Menus.FindAsync(id);

            var menuDto = new MenuGetDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName
            };

            if (menu == null)
            {
                return NotFound();
            }
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

            if (menu == null)
            {
                return NotFound();
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

            return foodItemsInMenu;
        }

        /// <summary>
        /// Edit a menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, ThAmCo.Catering.DTO.MenuPostDTO menu)
        {
            var thisMenu = await _context.Menus.FindAsync(id);

            if (thisMenu == null)
            {
                return NotFound();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

            _context.Menus.Add(thisMenu);
            await _context.SaveChangesAsync();

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
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
