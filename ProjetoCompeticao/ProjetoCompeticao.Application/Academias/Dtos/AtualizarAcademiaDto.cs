namespace ProjetoCompeticao.Application.Academias.Dtos
{
    public class AtualizarAcademiaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public AtualizarAcademiaDto() { }
    }
}
