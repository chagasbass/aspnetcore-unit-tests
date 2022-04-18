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
    /// <summary>
    /// Contém os Endpoints de Academia
    /// </summary>
    [Tags("Academias")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/academias")]
    public class AcademiasController : ApiBaseController
    {
        private readonly IAcademiaApplicationServices _academiaApplicationServices;

        public AcademiasController(ILogServices logServices,
                                      INotificationServices notificationServices,
                                      IAcademiaApplicationServices academiaApplicationServices) : base(logServices, notificationServices)
        {
            _academiaApplicationServices = academiaApplicationServices;
        }

        /// <summary>
        /// Efetua uma solitação para inserção de uma nova academia
        /// </summary>
        /// <param name="inserirAcademiaDto">Objeto que representa uma nova academia
        ///  - Nome - Nome da Academia
        ///  - Endereco -endereço da academia contendo Rua,número, bairro, cidade cep estado
        /// </param>
        ///  <remarks>
        /// Exemplo request:
        ///
        ///     POST /academias/
        ///     {
        ///        "Nome": "NOME_ACADEMIA",
        ///        "Endereco": "ENDEREÇO_ACADEMIA"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna quando a solitação de criação de academia é válida.</response>
        /// <response code="400">Retorna quando a solitação decriação de academia é inválida.</response>
        /// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        /// <returns>Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResult>> InserirAcademiaAsync([FromBody] InserirAcademiaDto inserirAcademiaDto)
        {
            var commandResult = (CommandResult)await _academiaApplicationServices.InserirAcademiaAsync(inserirAcademiaDto);

            return FormatApiResponse(commandResult);
        }

        /// <summary>
        /// Efetua uma solitação para atualização de uma nova academia
        /// </summary>
        /// <param name="atualizarAcademiaDto">Objeto que representa uma  academia
        ///  - Id - identificador da academia
        ///  - Nome - Nome da Academia
        ///  - Endereco -endereço da academia contendo Rua,número, bairro, cidade cep estado
        /// </param>
        ///  <remarks>
        /// Exemplo request:
        ///
        ///     PUT /academias/
        ///     {
        ///        "Id": "IDENTIFICADOR_ACADEMIA"
        ///        "Nome": "NOME_ACADEMIA",
        ///        "Endereco": "ENDEREÇO_ACADEMIA",
        ///     }
        /// </remarks>
        /// <response code="204">Retorna quando a solitação de atualização de academia é válida.</response>
        /// <response code="400">Retorna quando a solitação de atualização de academia é inválida.</response>
        /// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        /// <returns>Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        [HttpPut("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResult>> AtualizarAcademiaAsync([FromBody] AtualizarAcademiaDto atualizarAcademiaDto)
        {
            var commandResult = (CommandResult)await _academiaApplicationServices.AtualizarAcademiaAsync(atualizarAcademiaDto);

            return FormatApiResponse(commandResult);
        }

        /// <summary>
        /// Efetua uma solitação para exclusão de uma  academia
        /// </summary>
        /// <param name="id">Identificador da academia
        /// </param>
        ///  <remarks>
        /// Exemplo request:
        ///
        /// DELETE /academias/{Id}
        /// </remarks>
        /// <response code="200">Retorna quando a solitação de exclusão da academia é válida.</response>
        /// <response code="400">Retorna quando a solitação de exclusão da academia é inválida.</response>
        /// <response code="500">Retorna quando algum problema inesperado acontece na chamada.</response>
        /// <returns>Retorna um objeto do tipo CommandResult contendo o retorno da chamada</returns>
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResult>> ExcluirAcademiaAsync(Guid id)
        {
            var commandResult = (CommandResult)await _academiaApplicationServices.ExcluirAcademiaAsync(id);

            return FormatApiResponse(commandResult);
        }
    }
}
