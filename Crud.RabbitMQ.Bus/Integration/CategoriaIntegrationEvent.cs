using Crud.RabbitMQ.Bus.RabbitConfig;
using Crud.RabbitMQ.Dominio.Integration;
using EasyNetQ;

namespace Crud.RabbitMQ.Bus.Integration
{
    [Queue("", ExchangeName = ExchangesNames.NewCadastroCategoria)]
    public class CategoriaIntegrationEvent : IntegrationEvent
    {
        public CategoriaIntegrationEvent(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
