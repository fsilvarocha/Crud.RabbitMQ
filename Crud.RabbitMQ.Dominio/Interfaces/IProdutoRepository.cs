using Crud.RabbitMQ.Dominio.Entidades;

namespace Crud.RabbitMQ.Dominio.Interfaces;

public interface IProdutoRepository
{
    Task<Categoria> Salvar(Categoria produto);
}
