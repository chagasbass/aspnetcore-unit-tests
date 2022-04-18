using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCompeticao.Extensions.Logs.Services;
using ProjetoCompeticao.Shared.Entities;
using System.Text.Json;

namespace ProjetoCompeticao.Extensions.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogServices _logService;

        public GlobalExceptionHandlerMiddleware(ILogServices logService)
        {
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Responsavel por tratar as exceções geradas na aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const int statusCode = StatusCodes.Status500InternalServerError;
            const string dataType = @"application/problem+json";

            _logService.LogData.AddException(exception);
            _logService.LogData.AddResponseStatusCode(statusCode);
            _logService.WriteLog();
            _logService.WriteLogWhenRaiseExceptions();

            var problemDetails = new ProblemDetails
            {
                Title = "Um erro ocorreu ao processar o request.",
                Status = statusCode,
                Type = exception.GetBaseException().GetType().Name,
                Detail = $"Erro fatal na aplicação,entre em contato com um Desenvolvedor responsável. Causa: {exception.Message}",
                Instance = context.Request.HttpContext.Request.Path
            };

            var commandResult = new CommandResult(problemDetails);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = dataType;

            await context.Response.WriteAsync(JsonSerializer.Serialize(commandResult));
        }
    }
}
