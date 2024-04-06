using Crud.RabbitMQ.Dominio.DI;
using Crud.RabbitMQ.Dominio.Helpers;
using Crud.RabbitMQ.Infra.Context;
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
            ConfiguraAutoMapperConfiguration(services);
        }

        private static void ConfiguraAutoMapperConfiguration(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void ConfiguraIntegracoes(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        private static void ConfiguraRepositorios(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private static void ConfiguraServicos(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private static void ConfiguraDatabase(IServiceCollection services)
        {
            services.AddDbContext<ContextToLogado>(options =>
            {
                options.UseSqlServer(GetConfiguration<ConnectionString>("ConnectionString").Connection)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
            });
            services.AddTransient<ContextToLogado>();
        }

        public T GetService<T>() => _serviceProvider.GetService<T>();

        private static T GetConfiguration<T>(string secao)
        {
            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsetings.json")
                .Build()
                .GetSection(secao)
                .Get<T>();
        }
    }
}
