using Crud.RabbitMQ.Bus;
using Crud.RabbitMQ.Bus.Integration;
using Crud.RabbitMQ.Bus.RabbitConfig;
using Microsoft.AspNetCore.Mvc;

namespace Crud.RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMessageBus _messageBus;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            var retorno = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            foreach (var item in retorno)
            {
                var integrationEvent = new WeatherForecastIntegrationEvent(
                    item.Date, item.TemperatureC, item.TemperatureF, item.Summary);
                 _messageBus.Publish(integrationEvent, RountingKeysNames.Weather);

            }

            return retorno;
        }
    }
}
