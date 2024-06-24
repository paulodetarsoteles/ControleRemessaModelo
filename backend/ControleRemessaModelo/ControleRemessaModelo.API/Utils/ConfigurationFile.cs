using ControleRemessaModelo.API.Enums;
using ControleRemessaModelo.API.Excecoes;

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
                throw new CustomException(ErrorCode.ErroAoBuscarChaveNaConfiguracao);

            if (string.IsNullOrEmpty(secretKey))
                throw new CustomException(ErrorCode.ProblemaAoBuscarKeyNaConfig);

            return secretKey.ToCharArray();
        }
    }
}
