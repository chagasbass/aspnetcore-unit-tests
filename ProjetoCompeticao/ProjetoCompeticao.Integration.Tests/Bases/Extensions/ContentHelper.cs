using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ProjetoCompeticao.Integration.Tests.Bases.Extensions
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }
}
