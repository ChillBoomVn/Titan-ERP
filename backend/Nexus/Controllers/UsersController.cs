using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.API.Data;
using Nexus.API.Models;

namespace Nexus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API 1: Lấy danh sách tất cả nhân viên
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                                      .Include(u => u.Role) // Kèm theo thông tin Chức vụ
                                      .ToListAsync();
            return Ok(users);
        }

        // API 2: Lấy chi tiết 1 nhân viên theo ID
        // GET: api/Users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên này" });
            }

            return Ok(user);
        }
    }
}