using Crud.RabbitMQ.Dominio.Entidades;

namespace Crud.RabbitMQ.Service.Servicos.Interface;

public interface IProdutoService
{
    Task<Categoria> Salvar(Categoria produto);
}
