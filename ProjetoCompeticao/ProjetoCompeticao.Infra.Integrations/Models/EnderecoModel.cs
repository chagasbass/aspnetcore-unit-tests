using System.Text.Json.Serialization;

namespace ProjetoCompeticao.Infra.Integrations.Models
{
    public class EnderecoModel
    {
        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string? Logradouro { get; set; }

        [JsonPropertyName("complemento")]
        public string? Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string? Localidade { get; set; }

        [JsonPropertyName("uf")]
        public string? Uf { get; set; }

        [JsonPropertyName("ibge")]
        public string? Ibge { get; set; }

        [JsonInclude]
        [JsonPropertyName("gia")]
        public string? Gia { get; set; }

        [JsonPropertyName("ddd")]
        public string? DDD { get; set; }

        [JsonPropertyName("siafi")]
        public string? Siafi { get; set; }

        public EnderecoModel() { }
    }
}
