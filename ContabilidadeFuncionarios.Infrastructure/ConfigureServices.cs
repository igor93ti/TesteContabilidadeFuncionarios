using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using ContabilidadeFuncionarios.Domain.Services;
using ContabilidadeFuncionarios.Infrastructure.Data;
using ContabilidadeFuncionarios.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContabilidadeFuncionarios.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITaxaDescontoRepository, TaxaDescontoRepository>();
            services.AddScoped<ICalculoDescontoService, CalculoDescontoService>();
            services.AddScoped<IContrachequeService, ContrachequeService>();
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();

            return services;
        }
    }
}
