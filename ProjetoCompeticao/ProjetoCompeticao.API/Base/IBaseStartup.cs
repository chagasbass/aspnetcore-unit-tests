﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ProjetoCompeticao.Api.Bases
{
    public interface IBaseStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider);
        void ConfigureServices(IServiceCollection services);
    }
}
