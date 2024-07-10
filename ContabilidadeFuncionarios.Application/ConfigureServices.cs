using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace ContabilidadeFuncionarios.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
