using Crud.RabbitMQ.Dominio.Entidades;
using Crud.RabbitMQ.Dominio.Interfaces;
using Crud.RabbitMQ.Service.Servicos.Interface;

namespace Crud.RabbitMQ.Service.Servicos;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Categoria> Salvar(Categoria produto)=> await _produtoRepository.Salvar(produto);
}
