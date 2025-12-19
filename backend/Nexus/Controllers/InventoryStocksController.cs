using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.API.Data;
using Nexus.API.Models;

namespace Nexus.API.Controllers
{
    public class InventoryStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryStocksController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Warehouses (Lấy all kho)
        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            return Ok(await _context.InventoryStocks.ToListAsync());
        }


        // GET: api/Warehouses/5 (Lấy 1 kho)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByInventoryId(int id)
        {
            var inventory = await _context.InventoryStocks.FindAsync(id);
            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

        // POST: api/Warehouses (Tạo kho mới)
        [HttpPost]
        public async Task<IActionResult> Create(InventoryStock inventory)
        {
            _context.InventoryStocks.Add(inventory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByInventoryId), new { id = inventory.Id }, inventory);
        }

        // PUT: api/Warehouses/5 (Sửa kho)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventoryStock inventory)
        {
            if (id != inventory.Id) return BadRequest();
            _context.Entry(inventory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Warehouses/5 (Xóa kho)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _context.InventoryStocks.FindAsync(id);
            if (inventory == null) return NotFound();
            _context.InventoryStocks.Remove(inventory);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
