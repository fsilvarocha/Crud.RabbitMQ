using Crud.RabbitMQ.Dominio.Entidades;

namespace Crud.RabbitMQ.Service.Servicos.Interface;

public interface ICategoriaService
{
    Task<Categoria> Salvar(Categoria categoria);
}
