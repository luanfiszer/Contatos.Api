using Contatos.Application.Service;
using Contatos.Application.Service.Interface;
using Contatos.Data.Repository;
using Contatos.Domain.Interface;

namespace Contatos.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IContatoRepository, ContatoRepository>();

            return services;    
        }
    }
}
