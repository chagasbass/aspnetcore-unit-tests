
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProjetoCompeticao.Api.Bases;
using ProjetoCompeticao.Api.Extensions;
using ProjetoCompeticao.Extensions.DependencyInjection;
using ProjetoCompeticao.Extensions.Documentations;
using ProjetoCompeticao.Extensions.Healths;
using ProjetoCompeticao.Extensions.Middlewares;
using ProjetoCompeticao.Extensions.Performances;
using ProjetoCompeticao.Infra.Integrations.Extensions;

namespace ProjetoCompeticao.Api
{
    public class Startup : IBaseStartup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddMemoryCache()
                    .SolveAppDependencyInjections(Configuration)
                    .AddHttpClientResilience()
                    .SolveStructuralAppDependencyInjection()
                    .AddOptionsPattern(Configuration)
                    .AddGlobalExceptionHandlerMiddleware()
                    .AddCustomApiVersioning()
                    .AddSwaggerDocumentation()
                    .AddRequestResponseCompress()
                    .AddResponseRequestConfiguration()
                    .AddAppHealthChecks();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseResponseCompression();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseMiddleware<SerilogRequestLoggerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUIMultipleVersions(provider);

            app.UseCors(x => x.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.InsertHealthChecksMiddleware(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/monitor");
                //endpoints.MapHealthChecksUI(setup => setup.UIPath = "/monitor");
            });
        }
    }
}
