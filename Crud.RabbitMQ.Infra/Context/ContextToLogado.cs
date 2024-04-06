using Microsoft.EntityFrameworkCore;

namespace Crud.RabbitMQ.Infra.Context
{
    public class ContextToLogado : DbContext
    {
        public ContextToLogado(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
