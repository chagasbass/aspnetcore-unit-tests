using AutoFixture;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Tests.Bases;
using System.Collections.Generic;

namespace ProjetoCompeticao.Tests.Academias.Fakes
{
    public class ListarAcademiaDtoFake : IFake<ListarAcademiaDto>
    {
        private Fixture _fixture;

        public ListarAcademiaDtoFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public ListarAcademiaDto GerarEntidadeInvalida()
        {
            return null;
        }

        public ListarAcademiaDto GerarEntidadeValida()
        {
            return _fixture.Build<ListarAcademiaDto>().Create();
        }

        public List<ListarAcademiaDto> GerarListaValida(int quantidade)
        {
            var academias = new List<ListarAcademiaDto>();

            for (int iAcademias = 0; iAcademias < quantidade; iAcademias++)
            {
                academias.Add(GerarEntidadeValida());
            }

            return academias;

        }
    }
}
