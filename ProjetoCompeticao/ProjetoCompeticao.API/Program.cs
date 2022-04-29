using ProjetoCompeticao.Api;
using ProjetoCompeticao.Api.Extensions;
using ProjetoCompeticao.Extensions.Logs.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region configuring logs
Log.Logger = LogExtensions.ConfigureStructuralLog();
builder.Logging.AddSerilog(Log.Logger);
#endregion

try
{
    Log.Information("Iniciando a aplica��o");
    builder.UseStartup<Startup>();
}
catch (Exception ex)
{
    Log.Fatal($"Erro fatal na aplica��o => {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
