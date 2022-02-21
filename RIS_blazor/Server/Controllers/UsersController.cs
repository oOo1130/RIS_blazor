using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Services;

namespace RIS_blazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly CoreDbContext _context;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetUser")]
        public async Task<List<User>> GetAllUser()
        {
            return await new RISService(_context).GetAllUser();
        }
    }
}
