using Flunt.Notifications;
using ProjetoCompeticao.Application.Academias.Contracts;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Extensions;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Enums;
using ProjetoCompeticao.Shared.Notifications;

namespace ProjetoCompeticao.Application.ArtesMarciais.Services
{
    public class AcademiaApplicationServices : IAcademiaApplicationServices
    {
        private readonly INotificationServices _notificationServices;
        private readonly IReadAcademiaRepository _readAcademiaRepository;
        private readonly IWriteAcademiaRepository _writeAcademiaRepository;

        private bool _operacaoValida;

        public AcademiaApplicationServices(INotificationServices notificationServices,
                                           IReadAcademiaRepository readAcademiaRepository,
                                           IWriteAcademiaRepository writeAcademiaRepository)
        {
            _notificationServices = notificationServices;
            _readAcademiaRepository = readAcademiaRepository;
            _writeAcademiaRepository = writeAcademiaRepository;
        }

        public async Task<ICommandResult> AtualizarAcademiaAsync(AtualizarAcademiaDto atualizarAcademiaDto)
        {
            //Recebe contrato
            //valida contrato
            //se invalido, retorna objeto com erros
            //verifica se arte ja existe
            //se nao retorna objeto com mensagem de nao existencia
            //se sim atualiza academia
            //Status Code de retorno 204
            //Status code de retorno de erro 400

            #region Fail Fast Validation
            var academiaParaValidacao = new Academia(atualizarAcademiaDto.Nome, atualizarAcademiaDto.Endereco);

            if (!academiaParaValidacao.IsValid)
            {
                _notificationServices.AddNotifications(academiaParaValidacao.Notifications, StatusCodeOperation.BadRequest);

                return new CommandResult(academiaParaValidacao.Notifications, _operacaoValida, AcademiaMessagesExtensions.ErroAtualizacaoAcademia());
            }
            #endregion

            var academiaExistente = await _readAcademiaRepository.ListarAcademiasAsync(atualizarAcademiaDto.Id);

            if (academiaExistente is null)
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.BadRequest);
                _notificationServices.AddNotification(new Notification("academia", AcademiaMessagesExtensions.AcademiaNaoExiste()));

                return new CommandResult(_notificationServices.GetNotifications(), _operacaoValida, AcademiaMessagesExtensions.AcademiaNaoExiste());
            }

            academiaExistente.AlterarNome(atualizarAcademiaDto.Nome)
                             .AlterarEndereco(atualizarAcademiaDto.Endereco);

            await _writeAcademiaRepository.AtualizarAcademiaAsync(academiaExistente);

            _notificationServices.AddStatusCode(StatusCodeOperation.NoContent);

            _operacaoValida = true;

            return new CommandResult(academiaExistente, _operacaoValida, AcademiaMessagesExtensions.AtualizacaoDeAcademia());
        }

        public async Task<ICommandResult> ExcluirAcademiaAsync(Guid id)
        {
            //Recebe id
            //verifica se existe
            //se existe exclui, retorna mensagem de sucesso
            //se nao existe
            //retorna mensagem de erro
            //Status Code de retorno 200
            //Status code de retorno de erro 400

            var academiaExistente = await _readAcademiaRepository.ListarAcademiasAsync(id);

            if (academiaExistente is null)
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.BadRequest);

                _notificationServices.AddNotification(new Notification("academia", AcademiaMessagesExtensions.AcademiaNaoExiste()));

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.AcademiaNaoExiste());
            }

            await _writeAcademiaRepository.ExcluirAcademiaAsync(id);

            _operacaoValida = true;

            _notificationServices.AddStatusCode(StatusCodeOperation.Delete);

            return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.ExclusaoDeAcademia());
        }

        public async Task<ICommandResult> InserirAcademiaAsync(InserirAcademiaDto inserirAcademiaDto)
        {
            var novaAcademia = inserirAcademiaDto.ToEntity();

            if (!novaAcademia.IsValid)
            {
                _notificationServices.AddNotifications(novaAcademia.Notifications, StatusCodeOperation.BadRequest);

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.ErroInsercaoAcademia());
            }

            var academiaExistente = await _readAcademiaRepository.ListarAcademiasAsync(novaAcademia.Nome);

            if (academiaExistente is not null)
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.BadRequest);

                _notificationServices.AddNotification(new Notification("academia", AcademiaMessagesExtensions.AcademiaJaEstaCadastrada()));

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.ErroInsercaoAcademia());
            }

            await _writeAcademiaRepository.InserirAcademiaAsync(novaAcademia);

            _notificationServices.AddStatusCode(StatusCodeOperation.Post);

            _operacaoValida = true;

            return new CommandResult(novaAcademia, _operacaoValida, AcademiaMessagesExtensions.InsercaoDeAcademia());
        }

        public async Task<ICommandResult> ListarAcademiasAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _notificationServices.AddNotification(new Notification("academia", "O identificador é inválido"), StatusCodeOperation.BadRequest);

                return new CommandResult(_notificationServices.GetNotifications(), _operacaoValida, AcademiaMessagesExtensions.ErroPesquisaAcademia());
            }

            var academiaEncontrada = await _readAcademiaRepository.ListarAcademiasAsync(id);

            _operacaoValida = true;

            if (academiaEncontrada is null)
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.NotFound);

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());
            }

            var listarAcademiaDto = new ListarAcademiaDto
            {
                Id = academiaEncontrada.Id,
                Nome = academiaEncontrada.Nome,
                Endereco = academiaEncontrada.Endereco
            };

            _notificationServices.AddStatusCode(StatusCodeOperation.Get);

            return new CommandResult(listarAcademiaDto, _operacaoValida);
        }

        public async Task<ICommandResult> ListarAcademiasAsync(FiltroAcademiaDto filtroAcademiaDto)
        {
            var academias = await _readAcademiaRepository.ListarAcademiasAsync(filtroAcademiaDto.Pagina, filtroAcademiaDto.TamanhoPagina);

            _operacaoValida = true;

            if (!academias.Results.Any())
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.NotFound);

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());
            }

            _notificationServices.AddStatusCode(StatusCodeOperation.Get);

            return new CommandResult(academias, _operacaoValida);
        }

        public async Task<ICommandResult> ListarAcademiasAsync(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                _notificationServices.AddNotification(new Notification("academia", "O nome da academia é inválido"), StatusCodeOperation.BadRequest);

                return new CommandResult(_notificationServices.GetNotifications(), _operacaoValida, AcademiaMessagesExtensions.ErroPesquisaAcademia());
            }

            var academiaEncontrada = await _readAcademiaRepository.ListarAcademiasAsync(nome);

            _operacaoValida = true;

            if (academiaEncontrada is null)
            {
                _notificationServices.AddStatusCode(StatusCodeOperation.NotFound);

                return new CommandResult(_operacaoValida, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());
            }

            var listarAcademiaDto = new ListarAcademiaDto
            {
                Id = academiaEncontrada.Id,
                Nome = academiaEncontrada.Nome,
                Endereco = academiaEncontrada.Endereco
            };

            _notificationServices.AddStatusCode(StatusCodeOperation.Get);

            return new CommandResult(listarAcademiaDto, _operacaoValida);
        }

    }
}
