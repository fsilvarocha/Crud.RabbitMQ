using Crud.RabbitMQ.Dominio.Entidades;

namespace Crud.RabbitMQ.Dominio.Interfaces;

public interface ICategoriaRepository
{
    Task<Categoria> Salvar(Categoria categoria);
}
