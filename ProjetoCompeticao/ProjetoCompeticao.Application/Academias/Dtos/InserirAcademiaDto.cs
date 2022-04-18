using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Shared.Entities.Bases;

namespace ProjetoCompeticao.Application.Academias.Dtos
{
    public class InserirAcademiaDto : BaseDto<Academia>
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public InserirAcademiaDto() { }

        public override Academia ToEntity() => new(Nome, Endereco);
    }
}
