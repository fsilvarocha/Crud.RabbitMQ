using Crud.RabbitMQ.Dominio.Entidades;
using Crud.RabbitMQ.Dominio.Interfaces;
using Crud.RabbitMQ.Service.Servicos.Interface;

namespace Crud.RabbitMQ.Service.Servicos;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<Categoria> Salvar(Categoria categoria)=> await _categoriaRepository.Salvar(categoria);
}
