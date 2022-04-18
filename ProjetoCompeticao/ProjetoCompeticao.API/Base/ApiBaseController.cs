using Microsoft.AspNetCore.Mvc;
using ProjetoCompeticao.Extensions.Logs.Services;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Enums;
using ProjetoCompeticao.Shared.Notifications;
using System.Net;


namespace ProjetoCompeticao.Api.Bases
{
    /// <summary>
    /// Controller Base da aplicação
    /// </summary>
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly ILogServices _logService;
        private readonly INotificationServices _notificationServices;

        private string _defaultEndpointRoute;

        public ApiBaseController(ILogServices logService, INotificationServices notificationServices)
        {
            _logService = logService;
            _notificationServices = notificationServices;
        }

        internal ActionResult FormatApiResponse(CommandResult commandResult)
        {
            if (_notificationServices.HasNotifications())
            {
                var statusCodeOperation = _notificationServices.StatusCode;

                var notificacoes = _notificationServices.GetNotifications().ToList();

                commandResult.Data = notificacoes;

                _notificationServices.ClearNotifications();

                switch (statusCodeOperation)
                {
                    case var _ when statusCodeOperation == StatusCodeOperation.BadRequest:
                        GenerateLogResponse(commandResult, (int)HttpStatusCode.BadRequest);
                        return BadRequest(commandResult);
                    case var _ when statusCodeOperation == StatusCodeOperation.BusinessError:
                        GenerateLogResponse(commandResult, (int)HttpStatusCode.UnprocessableEntity);
                        return UnprocessableEntity(commandResult);
                    case var _ when statusCodeOperation == StatusCodeOperation.NotFound:
                        GenerateLogResponse(commandResult, (int)HttpStatusCode.NotFound);
                        return NotFound(commandResult);
                }
            }

            if (_notificationServices.StatusCode == StatusCodeOperation.Post)
                _defaultEndpointRoute = _logService.LogData.RequestUri;

            return FormatApiResponse(commandResult, _defaultEndpointRoute);
        }

        private void GenerateLogResponse(CommandResult commandResult, int statusCode)
        {
            _logService.LogData.AddResponseStatusCode(statusCode);
            _logService.LogData.AddResponseBody(commandResult);
            _logService.WriteLog();
        }

        private ActionResult FormatApiResponse(CommandResult commandResult, string defaultEndpointRoute = null)
        {
            var statusCodeOperation = _notificationServices.StatusCode;

            switch (statusCodeOperation)
            {
                case var _ when statusCodeOperation == StatusCodeOperation.Post:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.Created);
                    return Created(defaultEndpointRoute, commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Put:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.NoContent);
                    return NoContent();
                case var _ when statusCodeOperation == StatusCodeOperation.Get:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.OK);
                    return Ok(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Delete:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.OK);
                    return Ok(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Put:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.NotFound);
                    return NotFound(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.NotFound:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.NotFound);
                    return NotFound(commandResult);
                default:
                    return Ok(commandResult);
            }
        }
    }
}
