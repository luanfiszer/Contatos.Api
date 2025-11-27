using AutoMapper;
using Contatos.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ContatoProfile).Assembly);

            return services;
        }
    }
}
