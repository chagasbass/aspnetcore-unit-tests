using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoCompeticao.Infra.Data.DataContexts;
using System;
using System.Linq;
using System.Net.Http;

namespace ProjetoCompeticao.Integration.Tests.Bases.Configurations
{
    internal class TestApplication : WebApplicationFactory<Program>
    {
        private const string urlValue = @"https://localhost:5001/";

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDataContextTest");
                });

                var servicesProviders = services.BuildServiceProvider();

                using (var scope = servicesProviders.CreateScope())

                using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                {
                    try
                    {
                        if (appContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                            appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });

            return base.CreateHost(builder);
        }

        public new HttpClient CreateClient()
        {
            var _client = this.CreateDefaultClient();
            _client.BaseAddress = new Uri(urlValue);

            return _client;
        }
    }
}
