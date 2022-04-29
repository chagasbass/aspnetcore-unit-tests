using AutoFixture;
using ProjetoCompeticao.Tests.Academias.Fakes;
using Xunit;

namespace ProjetoCompeticao.Tests.Academias.Domain
{
    public class AcademiaTests
    {
        private readonly Fixture _Fixture;
        private readonly AcademiaFake _academiaFake;

        public AcademiaTests()
        {
            _Fixture = new Fixture();
            _academiaFake = new AcademiaFake(_Fixture);
        }

        [Fact]
        [Trait("Academia", "Validação de entidade")]
        public void Deve_Gerar_Notificação_Quando_Academia_For_Inválida()
        {
            #region Arrange , Act

            var academia = _academiaFake.GerarEntidadeInvalida();

            #endregion

            #region Act
            academia.Validate();
            #endregion

            #region Assert
            Assert.False(academia.IsValid);
            Assert.True(academia.Notifications.Count > 0);
            #endregion
        }

        [Fact]
        [Trait("Academia", "Validação de entidade")]
        public void Nao_Deve_Gerar_Notificação_Quando_Academia_For_Valida()
        {
            #region Arrange , Act

            var academia = _academiaFake.GerarEntidadeValida();

            #endregion

            #region Act
            academia.Validate();
            #endregion

            #region Assert
            Assert.True(academia.IsValid);
            Assert.False(academia.Notifications.Count > 0);
            #endregion
        }

    }
}
