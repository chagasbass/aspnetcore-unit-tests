using ProjetoCompeticao.Shared.Entities;
using System.Text.Json.Serialization;

namespace ProjetoCompeticao.Integration.Tests.Bases
{
    public class CommandResultTests : ICommandResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }

        public CommandResultTests() { }

    }
}
