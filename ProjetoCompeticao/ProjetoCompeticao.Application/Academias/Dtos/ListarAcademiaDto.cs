﻿namespace ProjetoCompeticao.Application.Academias.Dtos
{
    public class ListarAcademiaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public EnderecoDto Endereco { get; set; }

        public ListarAcademiaDto() { }

    }
}
