using Microsoft.AspNetCore.Mvc;
using RIS_blazor.Shared;
using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Services;
using RIS_blazor.Server.Models;


namespace RIS_blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CoreDbContext _context;


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetUser")]
        public async Task<List<User>> GetAllUser()
        {
            return await new RISService(_context).GetAllUser();
        }

        [HttpGet("get")]
        public string get()
        {
            return "sdf";
        }

    }
}