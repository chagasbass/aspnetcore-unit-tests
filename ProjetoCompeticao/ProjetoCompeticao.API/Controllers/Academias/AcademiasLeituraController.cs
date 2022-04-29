using Microsoft.AspNetCore.Mvc;
using ProjetoCompeticao.Api.Bases;
using ProjetoCompeticao.Application.Academias.Contracts;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Extensions.Logs.Services;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Notifications;
using System.Net.Mime;

namespace ProjetoCompeticao.API.Controllers.Academias
{
    [Tags("Academias")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/academias")]
    public class AcademiasLeituraController : ApiBaseController
    {
        private readonly IAcademiaApplicationServices _academiaApplicationServices;

        public AcademiasLeituraController(ILogServices logServices,
                                      INotificationServices notificationServices,
                                      IAcademiaApplicationServices academiaApplicationServices) : base(logServices, notificationServices)
        {
            _academiaApplicationServices = academiaApplicationServices;
        }

        /// <summary>
        /// Efetua uma solitação para listar  academia pelo identificador informado.
        /// </summary>
        /// <param name="id">Identificador de academia
        /// </param>
        /// <remarks>
        /// Exemplo request:
        ///GET /academias/{id}
        /// </remarks>
        /// <response code="200">Retorna quando a solitação de listagem de academia é válida.</response>
        /// <response code="400">Retorna quando a solitação de listagem de academia é inválida.</response>
        /// <response code="404">Retorna quando a solitação de listagem  academia não foi encontrada.</response>
        /// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        /// <returns>Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResult>> ListarAcademiasAsync(Guid id)
        {
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(id);

            return FormatApiResponse(commandResult);
        }

        ///// <summary>
        ///// Efetua uma solitação para listar  academia pelo nome informado.
        ///// </summary>
        ///// <param name="nome">nome da academia
        ///// </param>
        /////  <remarks>
        ///// Exemplo request:
        /////GET /academias/{nome}
        ///// </remarks>
        ///// <response code="200">Retorna quando a solitação de listagem de academia é válida.</response>
        ///// <response code="400">Retorna quando a solitação de listagem de academia é inválida.</response>
        ///// /// <response code="404">Retorna quando a solitação de listagem  academia não foi encontrada.</response>
        ///// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        ///// <returns>Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        //[HttpGet("{nome}")]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<CommandResult>> ListarAcademiasAsync(string nome)
        //{
        //    var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(nome);

        //    return FormatApiResponse(commandResult);
        //}

        /// <summary>
        /// Efetua uma solitação para listar  academias usando filtro de quantidade de registros e tamanho de páginas
        /// </summary>
        /// <param name="filtroAcademiaDto">objeto que representa os parâmetros de query para a pesquisa
        /// </param>
        /// <remarks>
        /// Exemplo request:
        /// GET/academias?pagina=VALOR_PAGINA?tamanhoPagina=TAMANHO_PAGINA
        /// </remarks>
        /// <response code="200">Retorna quando a solitação de listagem de academia é válida.</response>
        /// <response code="400">Retorna quando a solitação de listagem de academia é inválida.</response>
        /// <response code="404">Retorna quando a solitação de listagem  academia não foi encontrada.</response>
        /// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        /// <returns> Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        [HttpGet("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public ActionResult<CommandResult> ListarAcademias([FromQuery] FiltroAcademiaDto filtroAcademiaDto)
        {
            var commandResult = (CommandResult)_academiaApplicationServices.ListarAcademias(filtroAcademiaDto);

            return FormatApiResponse(commandResult);
        }
    }
}
