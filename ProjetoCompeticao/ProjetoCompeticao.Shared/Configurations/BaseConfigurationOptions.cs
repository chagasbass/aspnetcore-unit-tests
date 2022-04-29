namespace ProjetoCompeticao.Shared.Configurations
{
    public class BaseConfigurationOptions
    {
        public const string BaseConfig = "BaseConfiguration";
        public string NomeAplicacao { get; set; }
        public string StringConexaoBancoDeDados { get; set; }
        public string CepServiceUrl { get; set; }

        public BaseConfigurationOptions() { }

    }
}
