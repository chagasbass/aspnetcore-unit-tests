using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using ProjetoCompeticao.Extensions.Documentations.Filters;
using System.Reflection;


namespace ProjetoCompeticao.Extensions.Documentations
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            #region Criar versões diferentes de rotas
            services.AddSwaggerGen(c =>
            {
                #region aplicando o filtro de parâmetros não obrigatórios nas rotas no swagger
                c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                #endregion

                #region Resolver conflitos de rotas
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                #endregion

                #region Add comentários aos endpoints
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                #endregion
            });

            #endregion

            services.ConfigureOptions<SwaggerOptionsExtensions>();

            return services;
        }

        /// <summary>
        /// Extensão para configurar rotas iguais mas com versões diferentes
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        public static void UseSwaggerUIMultipleVersions(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
