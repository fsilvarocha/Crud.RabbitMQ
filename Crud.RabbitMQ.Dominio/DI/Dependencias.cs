namespace Crud.RabbitMQ.Dominio.DI
{
    public static class Dependencias
    {
        private static IResolver _resolver;
        private static readonly object _lock = new();

        public static IResolver Resolver
        {
            get
            {
                lock (_lock)
                {
                    if (_resolver == null)
                        throw new Exception("Nenhuma Instância de \"IResolvedor\" foi configurada no domínio");

                    return _resolver;
                }
            }

            set
            {
                lock (_lock)
                {
                    _resolver = value;
                }
            }
        }

    }
}
