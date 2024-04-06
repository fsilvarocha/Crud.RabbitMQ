using Crud.RabbitMQ.Dominio.Entidades;
using Crud.RabbitMQ.Dominio.Interfaces;
using Crud.RabbitMQ.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.RabbitMQ.Infra.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ContextToLogado _context;
    private readonly DbSet<Categoria> _categoria;

    public CategoriaRepository(ContextToLogado context)
    {
        _context = context;
        _categoria = _context.Set<Categoria>();
    }

    public async Task<Categoria> Salvar(Categoria categoria)
    {
        _categoria.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }
}
