namespace ProjetoCompeticao.Shared.Configurations
{
    public class HealthchecksConfigurationOptions
    {
        public const string BaseConfig = "HealthchecksConfiguration";

        public int SetEvaluationTimeInSeconds { get; set; }
        public int MaximumHistoryEntriesPerEndpoint { get; set; }
        public string ApiName { get; set; }

        public HealthchecksConfigurationOptions() { }

    }
}
