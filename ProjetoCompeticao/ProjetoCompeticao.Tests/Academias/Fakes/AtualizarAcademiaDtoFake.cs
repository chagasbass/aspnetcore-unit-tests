using AutoFixture;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Tests.Bases;

namespace ProjetoCompeticao.Tests.Academias.Fakes
{
    public class AtualizarAcademiaDtoFake : IFake<AtualizarAcademiaDto>
    {
        private readonly Fixture _fixture;

        public AtualizarAcademiaDtoFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public AtualizarAcademiaDto GerarEntidadeInvalida()
        {
            var entity = _fixture.Build<AtualizarAcademiaDto>()
                                 .Without(x => x.Nome)
                                 .Create();

            return entity;
        }

        public AtualizarAcademiaDto GerarEntidadeValida()
        {
            var entity = _fixture.Build<AtualizarAcademiaDto>()
                                 .Without(x => x.Nome)
                                 .Without(x => x.Endereco)
                                 .Do(x =>
                                 {
                                     x.Nome = "NOME_DA_ACADEMIA";
                                     x.Endereco = new EnderecoDto()
                                     {
                                         Bairro = "Bairro_Modelo",
                                         Cep = "24034-334",
                                         Cidade = "Cidade_Modelo",
                                         Estado = "Estado_Modelo",
                                         Numero = "33",
                                         Rua = "Rua_Modelo"
                                     };
                                 })
                                 .Create();
            return entity;
        }
    }
}
