using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRabbit.Users.Data.Context;
using MicroRabbit.Users.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Users.API.Controllers
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
        private IDisposable _db { get; }
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DbContextOptions options)
        {
            _logger = logger;
            _db = new UserDatabase(options);
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> Get()
        {
            var repo = new UserRepository(_db);
            var p = await repo.GetAll();
            return p;
        }
    }
}
