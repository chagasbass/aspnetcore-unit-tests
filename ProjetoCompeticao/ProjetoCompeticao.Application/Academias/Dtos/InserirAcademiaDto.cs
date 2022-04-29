using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Shared.Entities.Bases;

namespace ProjetoCompeticao.Application.Academias.Dtos
{
    public class InserirAcademiaDto : BaseDto<Academia>
    {
        public string Nome { get; set; }
        public EnderecoDto Endereco { get; set; }

        public InserirAcademiaDto() { }

        public override Academia ToEntity()
        {
            var endereco = Academia.PrepararEndereco(Endereco.Rua,
                                                     Endereco.Numero,
                                                     Endereco.Cep,
                                                     Endereco.Bairro,
                                                     Endereco.Cidade,
                                                     Endereco.Estado);

            return new(Nome, endereco);
        }
    }
}
