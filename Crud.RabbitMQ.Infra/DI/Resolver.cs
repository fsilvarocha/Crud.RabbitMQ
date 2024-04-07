using Crud.RabbitMQ.Bus;
using Crud.RabbitMQ.Dominio.DI;
using Crud.RabbitMQ.Dominio.Helpers;
using Crud.RabbitMQ.Dominio.Interfaces;
using Crud.RabbitMQ.Infra.Context;
using Crud.RabbitMQ.Infra.Repository;
using Crud.RabbitMQ.Service.Servicos;
using Crud.RabbitMQ.Service.Servicos.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Crud.RabbitMQ.Dominio.Helpers.Configuracoes;

namespace Crud.RabbitMQ.Infra.DI
{
    public class Resolver : IResolver
    {
        private static IServiceProvider _serviceProvider;

        public Resolver(IServiceCollection services, IConfiguration configuration)
        {
            ConfiguraDI(services, configuration);
        }

        public static void ConfiguraDI(IServiceCollection services, IConfiguration configuration)
        {
            ConfiguraDatabase(services);
            ConfiguraServicos(services);
            ConfiguraRepositorios(services);
            ConfiguraIntegracoes(services, configuration);
            ConfiguraRabbitMq(services);
            ConfiguraAutoMapperConfiguration(services);
        }

        private static void ConfiguraRabbitMq(IServiceCollection services)
        {
            var connection = GetConfiguration<ConnectionString>("ConnectionStrings").MessageBus;
            services.AddSingleton((IMessageBus)new MessageBus(connection));
        }

        private static void ConfiguraAutoMapperConfiguration(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void ConfiguraIntegracoes(IServiceCollection services, IConfiguration configuration)
        {

        }

        private static void ConfiguraRepositorios(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        }

        private static void ConfiguraServicos(IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
        }

        private static void ConfiguraDatabase(IServiceCollection services)
        {

            services.AddDbContext<ContextToLogado>(options =>
            {
                options.UseSqlServer(GetConfiguration<ConnectionString>("ConnectionStrings").Connection)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
            });
            services.AddTransient<ContextToLogado>();
        }

        public T GetService<T>() => _serviceProvider.GetService<T>();

        private static T GetConfiguration<T>(string secao)
        {
            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection(secao)
                .Get<T>();
        }
    }
}
