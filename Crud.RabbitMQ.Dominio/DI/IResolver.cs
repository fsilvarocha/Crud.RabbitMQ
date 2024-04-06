namespace Crud.RabbitMQ.Dominio.DI
{
    public interface IResolver
    {
        T GetService<T>();
    }
}
