using AutoFixture;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Tests.Bases;

namespace ProjetoCompeticao.Tests.Academias.Fakes
{
    public class EnderecoDtoFake : IFake<EnderecoDto>
    {
        private readonly Fixture _fixture;

        public EnderecoDtoFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public EnderecoDto GerarEntidadeInvalida()
        {
            return null;
        }

        public EnderecoDto GerarEntidadeValida()
        {
            var entity = _fixture.Build<EnderecoDto>()
                                 .Without(x => x.Rua)
                                 .Without(x => x.Numero)
                                 .Without(x => x.Cidade)
                                 .Without(x => x.Bairro)
                                 .Without(x => x.Estado)
                                 .Do(x =>
                                 {
                                     x.Rua = "Vila Pereira Carneiro";
                                     x.Cidade = "Niterói";
                                     x.Bairro = "Ponta da Areia";
                                     x.Estado = "RJ";
                                 }).Create();

            return entity;
        }
    }
}
