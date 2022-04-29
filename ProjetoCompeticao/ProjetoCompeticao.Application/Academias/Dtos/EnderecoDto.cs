namespace ProjetoCompeticao.Application.Academias.Dtos
{
    public class EnderecoDto
    {
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }

        public EnderecoDto() { }

        public static EnderecoDto PrepararEndereco(string endereco)
        {
            var dadosEndereco = endereco.Split('|');

            return new()
            {
                Rua = dadosEndereco[0],
                Numero = dadosEndereco[1],
                Bairro = dadosEndereco[2],
                Cidade = dadosEndereco[3],
                Cep = dadosEndereco[4],
                Estado = dadosEndereco[5]
            };
        }
    }
}
