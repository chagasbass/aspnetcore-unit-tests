using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Shared.Entities;

namespace ProjetoCompeticao.Application.Academias.PagedResults
{
    public class AcademiaPagedResults : PagedResults<ListarAcademiaDto>
    {
        private readonly IEnumerable<Academia> _academias;

        public AcademiaPagedResults(IEnumerable<Academia> academias)
        {
            _academias = academias;
            CriarResultados();
        }

        private void CriarResultados()
        {
            foreach (var academia in _academias)
            {
                var listarAcademiaDto = new ListarAcademiaDto()
                {
                    Id = academia.Id,
                    Nome = academia.Nome,
                    Endereco = EnderecoDto.PrepararEndereco(academia.Endereco)
                };

                Results.Add(listarAcademiaDto);
            }
        }
    }
}
