using ProjetoCompeticao.Application.Academias.Contracts;
using ProjetoCompeticao.Application.ArtesMarciais.Services;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Infra.Data.Academias.Repositories;
using ProjetoCompeticao.Infra.Data.DataContexts;

namespace ProjetoCompeticao.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection SolveAppDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<DataContext, DataContext>();
            services.AddScoped<IReadAcademiaRepository, ReadAcademiaRepository>();
            services.AddScoped<IWriteAcademiaRepository, WriteAcademiaRepository>();
            services.AddScoped<IAcademiaApplicationServices, AcademiaApplicationServices>();

            return services;
        }
    }
}
