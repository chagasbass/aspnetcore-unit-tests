using Microsoft.EntityFrameworkCore;
using ProjetoCompeticao.Application.Academias.Contracts;
using ProjetoCompeticao.Application.ArtesMarciais.Services;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Infra.Data.Academias.Repositories;
using ProjetoCompeticao.Infra.Data.DataContexts;
using ProjetoCompeticao.Infra.Integrations.Contracts;
using ProjetoCompeticao.Infra.Integrations.Services;

namespace ProjetoCompeticao.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection SolveAppDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["BaseConfiguration:StringConexaoBancoDeDados"];

            services.AddDbContext<DataContext>(contexto =>
            {
                contexto.UseSqlServer(connectionString);
            });

            services.AddScoped<IReadAcademiaRepository, ReadAcademiaRepository>();
            services.AddScoped<IWriteAcademiaRepository, WriteAcademiaRepository>();
            services.AddScoped<IAcademiaApplicationServices, AcademiaApplicationServices>();
            services.AddScoped<ICepServices, CepServices>();

            return services;
        }
    }
}
