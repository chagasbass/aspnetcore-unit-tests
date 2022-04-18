using System.Text.Json;

namespace ProjetoCompeticao.Extensions.Logs.Entities
{
    public class LogData
    {
        public DateTime Timestamp { get; private set; }
        public string RequestData { get; private set; }
        public string RequestMethod { get; private set; }
        public string RequestUri { get; private set; }
        public string ResponseData { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public string TraceId { get; private set; }
        public Exception Exception { get; private set; }

        public LogData()
        {
            Timestamp = DateTime.Now;
        }

        public LogData AddResponseStatusCode(int codigo)
        {
            ResponseStatusCode = codigo;
            return this;
        }

        public LogData AddRequestType(string metodo)
        {
            RequestMethod = metodo;
            return this;
        }

        public LogData AddRequestUrl(string url)
        {
            RequestUri = url;
            return this;
        }

        public LogData AddTraceIdentifier(string indentificador)
        {
            TraceId = indentificador;
            return this;
        }

        public LogData AddRequestBody(object requestData)
        {
            RequestData = JsonSerializer.Serialize(requestData);
            return this;
        }

        public LogData AddRequestBody(string requestData)
        {
            RequestData = requestData;
            return this;
        }

        public LogData AddResponseBody(object responseData)
        {
            ResponseData = JsonSerializer.Serialize(responseData);
            return this;
        }

        public LogData AddResponseBody(string responseData)
        {
            ResponseData = responseData;
            return this;
        }

        public LogData AddException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        public LogData ClearLogData()
        {
            Timestamp = DateTime.Now;
            RequestData = string.Empty;
            RequestMethod = string.Empty;
            RequestUri = string.Empty;
            ResponseData = string.Empty;
            ResponseStatusCode = 0;
            TraceId = string.Empty;
            Exception = default;

            return this;
        }
    }
}
