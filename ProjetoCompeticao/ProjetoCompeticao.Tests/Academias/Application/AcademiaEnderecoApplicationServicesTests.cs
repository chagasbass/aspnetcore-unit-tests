using AutoFixture;
using Flunt.Notifications;
using Moq;
using Moq.AutoMock;
using ProjetoCompeticao.Application.Academias.Services;
using ProjetoCompeticao.Infra.Integrations.Contracts;
using ProjetoCompeticao.Infra.Integrations.Models;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Notifications;
using ProjetoCompeticao.Tests.Academias.Fakes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoCompeticao.Tests.Academias.Application
{
    public class AcademiaEnderecoApplicationServicesTests
    {
        private readonly Fixture _fixture;
        private readonly EnderecoDtoFake _enderecoDtoFake;
        private readonly AutoMocker _autoMocker;
        private readonly AcademiaEnderecoApplicationServices _academiaApplicationServices;


        public AcademiaEnderecoApplicationServicesTests()
        {
            _fixture = new Fixture();
            _enderecoDtoFake = new EnderecoDtoFake(_fixture);
            _autoMocker = new AutoMocker();
            _academiaApplicationServices = _autoMocker.CreateInstance<AcademiaEnderecoApplicationServices>();
        }

        [Fact]
        [Trait("AcademiaEnderecoApplicationServices", "Busca de endereço por cep informado")]
        public async Task Deve_Retornar_Endereco_Quando_Cep_For_Válido()
        {
            #region Arrange

            var cepInformado = "24040-110";

            var enderecoModel = new EnderecoModel
            {
                Cep = "24040-110",
                Logradouro = "Vila Pereira Carneiro",
                Complemento = "",
                Bairro = "Ponta D'Areia",
                Localidade = "Niterói",
                Uf = "RJ",
                Ibge = "3303302",
                Gia = "",
                DDD = "21",
                Siafi = "5865"
            };

            _autoMocker.GetMock<ICepServices>().Setup(x => x.RecuperarEnderecoPorCepAsync(cepInformado)).ReturnsAsync(enderecoModel);

            #endregion

            #region Act

            var commandResult = (CommandResult)await _academiaApplicationServices.BuscarEnderecoPorCepAsync(cepInformado);

            #endregion

            #region Assert
            Assert.True(commandResult.Success);
            Assert.NotNull(commandResult.Data);

            #endregion
        }

        [Fact]
        [Trait("AcademiaEnderecoApplicationServices", "Busca de endereço por cep informado")]
        public async Task Deve_Retornar_Notificação_Quando_Cep_For_Invalido()
        {
            #region Arrange

            var cepInformado = string.Empty;

            var enderecoModel = new EnderecoModel
            {
                Cep = "24040-110",
                Logradouro = "Vila Pereira Carneiro",
                Complemento = "",
                Bairro = "Ponta D'Areia",
                Localidade = "Niterói",
                Uf = "RJ",
                Ibge = "3303302",
                Gia = "",
                DDD = "21",
                Siafi = "5865"
            };

            var notifications = new List<Notification>() { new Notification("cep", "O cep é inválido") };

            _autoMocker.GetMock<ICepServices>().Setup(x => x.RecuperarEnderecoPorCepAsync(cepInformado)).ReturnsAsync(enderecoModel);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.GetNotifications()).Returns(notifications);
            #endregion

            #region Act

            var commandResult = (CommandResult)await _academiaApplicationServices.BuscarEnderecoPorCepAsync(cepInformado);

            var _notification = (Notification)commandResult.Data;

            #endregion

            #region Assert
            Assert.False(commandResult.Success);
            Assert.NotNull(commandResult.Data);
            Assert.Equal("O cep é inválido", _notification.Message);

            #endregion
        }

        [Fact]
        [Trait("AcademiaEnderecoApplicationServices", "Busca de endereço por cep informado")]
        public async Task Deve_Retornar_Notificação_Quando_Endereco_Não_For_Encontrado_Pelo_Cep_Informado()
        {
            #region Arrange

            var cepInformado = "3333-3456";

            EnderecoModel enderecoModel = null;

            var notifications = new List<Notification>() { new Notification("cep", "Cep não encontrado") };

            _autoMocker.GetMock<ICepServices>().Setup(x => x.RecuperarEnderecoPorCepAsync(cepInformado)).ReturnsAsync(enderecoModel);
            _autoMocker.GetMock<INotificationServices>().Setup(x => x.GetNotifications()).Returns(notifications);
            #endregion

            #region Act

            var commandResult = (CommandResult)await _academiaApplicationServices.BuscarEnderecoPorCepAsync(cepInformado);

            var _notification = (Notification)commandResult.Data;

            #endregion

            #region Assert
            Assert.False(commandResult.Success);
            Assert.NotNull(commandResult.Data);
            Assert.Equal("Cep não encontrado", _notification.Message);

            #endregion
        }

    }
}
