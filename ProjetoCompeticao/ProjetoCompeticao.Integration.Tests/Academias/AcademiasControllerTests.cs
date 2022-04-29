using AutoFixture;
using ProjetoCompeticao.Domain.Academias.Extensions;
using ProjetoCompeticao.Integration.Tests.Bases;
using ProjetoCompeticao.Integration.Tests.Bases.Configurations;
using ProjetoCompeticao.Integration.Tests.Bases.Extensions;
using ProjetoCompeticao.Tests.Academias.Fakes;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoCompeticao.Integration.Tests.Academias
{
    public class AcademiasControllerTests
    {
        private readonly Fixture _fixture;
        private readonly InserirAcademiaDtoFake _inserirAcademiaDtoFake;
        private readonly TestApplication _app;

        public AcademiasControllerTests()
        {
            _fixture = new Fixture();
            _inserirAcademiaDtoFake = new InserirAcademiaDtoFake(_fixture);
            _app = new TestApplication();
        }

        [Fact]
        [Trait("AcademiasController", "Criar uma nova Academia")]
        public async Task Deve_Retornar_Um_CommandResult_Contendo_Os_Dados_Da_Nova_Academia()
        {
            #region Arrange

            var academiaDto = _inserirAcademiaDtoFake.GerarEntidadeValida();

            var postContent = ContentHelper.GetStringContent(academiaDto);

            var _client = _app.CreateClient();

            #endregion

            #region Act
            var response = await _client.PostAsync("v1/academias", postContent);

            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync();

            var commandResultTests = JsonSerializer.Deserialize<CommandResultTests>(stringResult);

            #endregion

            #region Assert

            Assert.Equal(commandResultTests?.Message, AcademiaMessagesExtensions.InsercaoDeAcademia());
            Assert.NotNull(commandResultTests);

            #endregion
        }
    }
}
