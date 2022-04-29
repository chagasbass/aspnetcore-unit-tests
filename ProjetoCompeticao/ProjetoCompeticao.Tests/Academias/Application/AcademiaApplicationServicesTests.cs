using AutoFixture;
using Flunt.Notifications;
using Moq;
using Moq.AutoMock;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Application.Academias.PagedResults;
using ProjetoCompeticao.Application.ArtesMarciais.Services;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Extensions;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Enums;
using ProjetoCompeticao.Shared.Notifications;
using ProjetoCompeticao.Tests.Academias.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoCompeticao.Tests.Academias.Application
{
    public class AcademiaApplicationServicesTests
    {
        /*
         *Fixture = objeto para  criar os fakes dos objetos
         *AcademiaFake = objeto que representa o fake de uma entidade academia
         *AutoMocker = objeto que cria a instancia da classe que será testada e resolve as suas dependências
         */

        private readonly Fixture _fixture;
        private readonly AcademiaFake _academiaFake;
        private readonly AtualizarAcademiaDtoFake _atualizarAcademiaDtoFake;
        private readonly InserirAcademiaDtoFake _inserirAcademiaDtoFake;
        private readonly ListarAcademiaDtoFake _listarAcademiaDtoFake;
        private readonly AutoMocker _autoMocker;
        private readonly AcademiaApplicationServices _academiaApplicationServices;

        public AcademiaApplicationServicesTests()
        {
            _fixture = new Fixture();
            _academiaFake = new AcademiaFake(_fixture);
            _atualizarAcademiaDtoFake = new AtualizarAcademiaDtoFake(_fixture);
            _inserirAcademiaDtoFake = new InserirAcademiaDtoFake(_fixture);
            _listarAcademiaDtoFake = new ListarAcademiaDtoFake(_fixture);
            _autoMocker = new AutoMocker();
            _academiaApplicationServices = _autoMocker.CreateInstance<AcademiaApplicationServices>();
        }

        #region Atualização de Academias

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de contrato ao atualizar")]
        public async Task Deve_Retornar_Notificação_Quando_Contrato_De_Atualização_For_Invalido()
        {
            #region Arrange
            var atualizarAcademiaDto = _atualizarAcademiaDtoFake.GerarEntidadeInvalida();

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);

            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.AtualizarAcademiaAsync(atualizarAcademiaDto);

            #endregion

            #region Assert

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao atualizar")]
        public async Task Deve_Retornar_Notificação_Quando_Academia_Atualizada_Nao_Existe()
        {
            #region Arrange
            var atualizarAcademiaDto = _atualizarAcademiaDtoFake.GerarEntidadeValida();
            Academia academiaEncontrada = null;

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);
            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(atualizarAcademiaDto.Id)).ReturnsAsync(academiaEncontrada);

            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.AtualizarAcademiaAsync(atualizarAcademiaDto);

            #endregion

            #region Assert

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao atualizar")]
        public async Task Deve_Atualizar_Academia_Quando_Contrato_Estiver_Válido_E_Academia_Existir()
        {
            #region Arrange
            var atualizarAcademiaDto = _atualizarAcademiaDtoFake.GerarEntidadeValida();
            Academia academiaEncontrada = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.NoContent);
            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(atualizarAcademiaDto.Id)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<IWriteAcademiaRepository>().Setup(x => x.AtualizarAcademiaAsync(academiaEncontrada)).ReturnsAsync(academiaEncontrada);

            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.AtualizarAcademiaAsync(atualizarAcademiaDto);

            #endregion

            #region Assert

            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(atualizarAcademiaDto.Id), Times.Once);
            _autoMocker.GetMock<IWriteAcademiaRepository>().Verify(x => x.AtualizarAcademiaAsync(academiaEncontrada), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.NoContent);

            #endregion
        }

        #endregion

        #region Exclusão de Academias

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao excluir")]
        public async Task Deve_Retornar_Notificação_Quando_Academia_A_Ser_Excluída_Nao_Existir()
        {
            #region Arrange

            var idExclusao = Guid.NewGuid();

            Academia academiaRecuperada = null;

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(idExclusao)).ReturnsAsync(academiaRecuperada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ExcluirAcademiaAsync(idExclusao);
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(idExclusao), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.AcademiaNaoExiste());

            #endregion

        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao excluir")]
        public async Task Deve_Excluir_Academia()
        {
            #region Arrange

            var idExclusao = Guid.NewGuid();

            var academiaRecuperada = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(idExclusao)).ReturnsAsync(academiaRecuperada);
            _autoMocker.GetMock<IWriteAcademiaRepository>().Setup(x => x.ExcluirAcademiaAsync(academiaRecuperada)).Returns(Task.CompletedTask);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.Delete);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ExcluirAcademiaAsync(idExclusao);
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(idExclusao), Times.Once);
            _autoMocker.GetMock<IWriteAcademiaRepository>().Verify(x => x.ExcluirAcademiaAsync(academiaRecuperada), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.Delete);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.ExclusaoDeAcademia());

            #endregion
        }

        #endregion

        #region Inserção de Academias

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao inserir")]
        public async Task Deve_Retornar_Notificação_Quando_Ao_Inserir_A_Academia_For_Inválida()
        {
            #region Arrange
            var inserirAcademiaDto = _inserirAcademiaDtoFake.GerarEntidadeInvalida();

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);

            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.InserirAcademiaAsync(inserirAcademiaDto);

            #endregion

            #region Assert

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.ErroInsercaoAcademia());

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao inserir")]
        public async Task Deve_Retornar_Notificação_Quando_Ao_Inserir_A_Academia_Já_Existir()
        {
            #region Arrange
            var inserirAcademiaDto = _inserirAcademiaDtoFake.GerarEntidadeValida();
            var academiaEcontrada = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);
            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(inserirAcademiaDto.Nome)).ReturnsAsync(academiaEcontrada);

            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.InserirAcademiaAsync(inserirAcademiaDto);

            #endregion

            #region Assert


            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(inserirAcademiaDto.Nome), Times.Once);
            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.ErroInsercaoAcademia());

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Validação de academia ao inserir")]
        public async Task Deve_Inserir_Uma__Nova_Academia_Quando_Academia_For_Valida()
        {
            #region Arrange
            var inserirAcademiaDto = _inserirAcademiaDtoFake.GerarEntidadeValida();
            Academia academiaEcontrada = null;
            var novaAcademia = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.Post);
            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(inserirAcademiaDto.Nome)).ReturnsAsync(academiaEcontrada);
            _autoMocker.GetMock<IWriteAcademiaRepository>().Setup(x => x.InserirAcademiaAsync(novaAcademia)).ReturnsAsync(novaAcademia);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.InserirAcademiaAsync(inserirAcademiaDto);

            #endregion

            #region Assert

            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(inserirAcademiaDto.Nome), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.Post);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.InsercaoDeAcademia());

            #endregion
        }


        #endregion

        #region Listagem De Academias

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public void Deve_Listar_As_Academias_Usando_Paginação()
        {
            #region Arrange

            var filtroAcademiaDto = new FiltroAcademiaDto();

            var academiasEncontradas = new List<Academia>();
            academiasEncontradas = _academiaFake.GerarListaDeAcademiasValidas(20);

            var listarAcademiasDto = _listarAcademiaDtoFake.GerarListaValida(20);

            var resultadosPaginados = new AcademiaPagedResults(academiasEncontradas);
            var retornoAcademias = new PagedResults<Academia>
            {
                Results = academiasEncontradas
            };

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademias(filtroAcademiaDto.Pagina, filtroAcademiaDto.TamanhoPagina)).Returns(retornoAcademias);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.Get);
            #endregion

            #region Act
            var commandResult = (CommandResult)_academiaApplicationServices.ListarAcademias(filtroAcademiaDto);
            var pagedResults = (AcademiaPagedResults)commandResult.Data;
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademias(filtroAcademiaDto.Pagina, filtroAcademiaDto.TamanhoPagina), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.Get);
            Assert.True(pagedResults.Results.Any());

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public void Nao_Deve_Retornar_Listagem_De_Academias_Quando_Listagem_de_Academias_Não_Existir()
        {
            #region Arrange

            var filtroAcademiaDto = new FiltroAcademiaDto();

            var academiasEncontradas = new List<Academia>();

            var listarAcademiasDto = new ListarAcademiaDto();

            var retornoAcademias = new PagedResults<Academia>
            {
                Results = academiasEncontradas
            };

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademias(filtroAcademiaDto.Pagina, filtroAcademiaDto.TamanhoPagina)).Returns(retornoAcademias);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.NotFound);
            #endregion

            #region Act
            var commandResult = (CommandResult)_academiaApplicationServices.ListarAcademias(filtroAcademiaDto);

            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademias(filtroAcademiaDto.Pagina, filtroAcademiaDto.TamanhoPagina), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.NotFound);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Deve_Listar_A_Academia_Pelo_Id_Informado()
        {
            #region Arrange

            var academiaId = Guid.NewGuid();

            var academiaEncontrada = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(academiaId)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.Get);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(academiaId);
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(academiaId), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.Get);
            Assert.IsAssignableFrom<ListarAcademiaDto>(commandResult.Data);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Deve_Retornar_Notificação_Quando_Ao_Listar_A_Academia_Pelo_O_Id_For_Inválido()
        {
            #region Arrange

            var academiaId = Guid.Empty;

            var mensagemDeRetorno = "O nome da academia é inválido";

            Academia academiaEncontrada = null;

            var notifications = new List<Notification>
            {
                new Notification("academia", "O nome da academia é inválido")
            };

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(academiaId)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.GetNotifications()).Returns(notifications);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(academiaId);
            var commandData = (List<Notification>)commandResult.Data;
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(academiaId), Times.Never);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);
            Assert.True(commandData.Any());
            Assert.Equal(commandData[0].Message, mensagemDeRetorno);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Não_Deve_Listar_A_Academia_Pelo_Id_Informado_Quando_Academia_Não_For_Encontrada()
        {
            #region Arrange

            var academiaId = Guid.NewGuid();

            Academia academiaEncontrada = null;

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(academiaId)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.NotFound);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(academiaId);
            var commandData = (List<Notification>)commandResult.Data;
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(academiaId), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.NotFound);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Deve_Listar_A_Academia_Pelo_Nome_Informado()
        {
            #region Arrange

            var nomeAcademia = "MINHA_ACADEMIA";

            var academiaEncontrada = _academiaFake.GerarEntidadeValida();

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(nomeAcademia)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.Get);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(nomeAcademia);
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(nomeAcademia), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.Get);
            Assert.IsAssignableFrom<ListarAcademiaDto>(commandResult.Data);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Deve_Retornar_Notificação_Quando_Ao_Listar_A_Academia_Pelo_O_Nome_For_Inválido()
        {
            #region Arrange

            var nomeAcademia = string.Empty;

            var mensagemDeRetorno = "O nome da academia é inválido";

            var notifications = new List<Notification>
            {
                new Notification("academia", "O nome da academia é inválido")
            };

            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.BadRequest);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.GetNotifications()).Returns(notifications);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(nomeAcademia);
            var commandData = (List<Notification>)commandResult.Data;
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(nomeAcademia), Times.Never);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.BadRequest);
            Assert.True(commandData.Any());
            Assert.Equal(commandData[0].Message, mensagemDeRetorno);

            #endregion
        }

        [Fact]
        [Trait("AcademiaApplicationServices", "Listagem de academias")]
        public async Task Não_Deve_Listar_A_Academia_Pelo_Nome_Informado_Quando_Academia_Não_For_Encontrada()
        {
            #region Arrange

            var nomeAcademia = "ACADEMIA_NAO_ENCONTRADA";

            Academia academiaEncontrada = null;

            _autoMocker.GetMock<IReadAcademiaRepository>().Setup(x => x.ListarAcademiasAsync(nomeAcademia)).ReturnsAsync(academiaEncontrada);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.StatusCode).Returns(StatusCodeOperation.NotFound);
            #endregion

            #region Act
            var commandResult = (CommandResult)await _academiaApplicationServices.ListarAcademiasAsync(nomeAcademia);
            var commandData = (List<Notification>)commandResult.Data;
            #endregion

            #region Assert
            _autoMocker.GetMock<IReadAcademiaRepository>().Verify(x => x.ListarAcademiasAsync(nomeAcademia), Times.Once);

            Assert.NotNull(commandResult);
            Assert.Equal(_autoMocker.GetMock<INotificationServices>().Object.StatusCode, StatusCodeOperation.NotFound);
            Assert.Equal(commandResult.Message, AcademiaMessagesExtensions.PesquisaDeAcademiasSemRetorno());

            #endregion
        }

        #endregion
    }
}
