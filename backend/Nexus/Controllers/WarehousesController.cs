using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.API.Data;
using Nexus.API.Models;

namespace Nexus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Warehouses (Lấy tất cả kho)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Warehouses.ToListAsync());
        }

        // GET: api/Warehouses/5 (Lấy 1 kho)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return NotFound();
            return Ok(warehouse);
        }

        // POST: api/Warehouses (Tạo kho mới)
        [HttpPost]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = warehouse.Id }, warehouse);
        }

        // PUT: api/Warehouses/5 (Sửa kho)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id) return BadRequest();
            _context.Entry(warehouse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Warehouses/5 (Xóa kho)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return NotFound();
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}