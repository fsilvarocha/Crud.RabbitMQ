namespace Crud.RabbitMQ.Dominio.Helpers;

public class Configuracoes
{

    public class ConnectionString
    {
        public string Connection { get; set; }
        public string ProviderName { get; set; }
        public string MessageBus { get; set; }
    }
}
