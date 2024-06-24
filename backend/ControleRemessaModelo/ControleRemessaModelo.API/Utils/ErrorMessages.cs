namespace ControleRemessaModelo.API.Utils
{
    public static class ErrorMessages
    {
        //Autenticação
        public const string None = "None";
        public const string UsuarioNaoEncontrado = "Usuário não encontrado";
        public const string UsuarioOuSenhaInvalida = "Usuário ou senha inválida";
        public const string UsuarioBloqueado = "Usuário Bloqueado";
        public const string UsuarioComPendencia = "Usuário com pendência";
        public const string ProblemaNoServicoAutenticacao = "Problema no serviço de autenticação";

        //Arquivo de configuração
        public const string ErroAoBuscarChaveNaConfiguracao = "Chave secreta não encontrada na configuração";
        public const string ProblemaAoBuscarKeyNaConfig = "Problema ao buscar chave secreta na configuração";
        public const string ErroAoBuscarValorDaKey = "Problema ao pegar valor da chave";
        public const string ErroAoConverterValorDaConfig = "Erro ao converter valor da chave de expiração";
    }
}
