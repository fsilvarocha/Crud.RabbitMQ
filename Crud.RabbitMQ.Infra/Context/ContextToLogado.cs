﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Crud.RabbitMQ.Infra.Context
{
    public class ContextToLogado : DbContext
    {
        public ContextToLogado(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var mappingTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var mappingType in mappingTypes)
            {
                dynamic instance = Activator.CreateInstance(mappingType);
                modelBuilder.ApplyConfiguration(instance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
