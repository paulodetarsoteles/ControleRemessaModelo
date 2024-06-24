namespace ControleRemessaModelo.API.Utils
{
    public static class ConfigurationFile
    {
        private readonly static IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static char[] GetConfigurationKey(string key)
        {
            string secretKey = configuration[key] ??
                throw new Exception(ErrorMessages.ErroAoBuscarChaveNaConfiguracao);

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception(ErrorMessages.ProblemaAoBuscarKeyNaConfig);

            return secretKey.ToCharArray();
        }
    }
}
