using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Mediator.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ACP.Api.Controllers
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
        private readonly IMediator _mediator;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var loginQuery = new LoginQuery("test", "test");
            var result = await _mediator.SendAsync(loginQuery);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("helloApi")]
        public async Task<IActionResult> PonPon()
        {
            var loginQuery = new RegisterCommand("test", "test", "test", "test");
            var result = await _mediator.SendAsync(loginQuery);
            return Ok(result);
        }
    }
}