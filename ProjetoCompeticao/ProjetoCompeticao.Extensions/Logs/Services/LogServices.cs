using ProjetoCompeticao.Extensions.Logs.Entities;
using Serilog;
using Serilog.Context;
using System.Text;

namespace ProjetoCompeticao.Extensions.Logs.Services
{
    public class LogServices : ILogServices
    {
        public LogData LogData { get; set; }

        private readonly ILogger _logger = Log.ForContext<LogServices>();

        public LogServices()
        {
            LogData = new LogData();
        }

        public void WriteLog()
        {
            var logInformation = new StringBuilder();

            using (LogContext.PushProperty("Log da operação", ""))
            {
                logInformation.AppendLine($"[API Request Data]:{ LogData.RequestData }");
                logInformation.AppendLine($"[API Request Trace Id]:{LogData.TraceId}");
                logInformation.AppendLine($"[API Response Data]:{LogData.ResponseData}");
                logInformation.AppendLine($"[API Response StatusCode]:{ LogData.ResponseStatusCode }");

                _logger.Information(logInformation.ToString());
            }

            LogData.ClearLogData();

        }

        public void CreateStructuredLog(LogData logData) => LogData = logData;

        public void WriteLogWhenRaiseExceptions()
        {

            if (LogData is not null)
            {
                using (LogContext.PushProperty("Log da operação", ""))
                {
                    if (LogData.Exception is not null)
                    {
                        var logInformation = new StringBuilder();

                        logInformation.AppendLine($"[API Exception]: { LogData.Exception.GetType().Name }");
                        logInformation.AppendLine($"[API Message]: { LogData.Exception.Message }");
                        logInformation.AppendLine($"[API ExceptionStackTrace]: { LogData.Exception.StackTrace }");

                        if (LogData?.Exception.InnerException is not null)
                        {
                            logInformation.AppendLine($"[InnerException]: { LogData.Exception?.InnerException?.Message }");
                        }

                        _logger.Error(logInformation.ToString());
                    }
                }

                LogData.ClearLogData();
            }

        }

        public void WriteMessage(string message) => _logger.Information($"{message}");

    }
}
