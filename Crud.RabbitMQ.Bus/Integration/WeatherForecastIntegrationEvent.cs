using Crud.RabbitMQ.Bus.RabbitConfig;
using Crud.RabbitMQ.Dominio.Integration;
using EasyNetQ;

namespace Crud.RabbitMQ.Bus.Integration
{
    [Queue("", ExchangeName = ExchangesNames.NewWeatherForecast)]
    public class WeatherForecastIntegrationEvent : IntegrationEvent
    {
        public WeatherForecastIntegrationEvent(DateOnly date, int temperatureC, int temperatureF, string? summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            TemperatureF = temperatureF;
            Summary = summary;
        }

        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}
