using AutoFixture;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Tests.Bases;
using System.Collections.Generic;

namespace ProjetoCompeticao.Tests.Academias.Fakes
{
    public class AcademiaFake : IFake<Academia>
    {
        private readonly Fixture _fixture;

        public AcademiaFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public Academia GerarEntidadeInvalida()
        {
            var entity = _fixture.Build<Academia>()
                                 .Do(x =>
                                 {
                                     x.AlterarEndereco("Meu endereço");
                                 }).Create();

            entity.AlterarNome("");

            return entity;
        }

        public Academia GerarEntidadeValida()
        {
            var entity = _fixture.Build<Academia>().Create();
            entity.AlterarEndereco("Rua Conde Pereira Carneiro|52|Ponta da Areira|Niterói|24040-110|Rio de Janeiro");

            return entity;
        }

        public List<Academia> GerarListaDeAcademiasValidas(int quantidade)
        {
            var academias = new List<Academia>();

            for (int iAcademias = 0; iAcademias < quantidade; iAcademias++)
            {
                academias.Add(GerarEntidadeValida());
            }

            return academias;
        }
    }
}
