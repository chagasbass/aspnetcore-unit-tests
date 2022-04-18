using ProjetoCompeticao.Extensions.Logs.Entities;

namespace ProjetoCompeticao.Extensions.Logs.Services
{
    public interface ILogServices
    {
        public LogData LogData { get; set; }
        void WriteLog();
        void CreateStructuredLog(LogData logData);
        void WriteLogWhenRaiseExceptions();
        void WriteMessage(string message);
    }
}
