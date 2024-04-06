using Crud.RabbitMQ.Dominio.Entidades;
using Crud.RabbitMQ.Dominio.Interfaces;
using Crud.RabbitMQ.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.RabbitMQ.Infra.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ContextToLogado _context;
    private readonly DbSet<Categoria> _produtos;

    public ProdutoRepository(ContextToLogado context)
    {
        _context = context;
        _produtos = _context.Set<Categoria>();
    }

    public async Task<Categoria> Salvar(Categoria produto)
    {
        _produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }
}
