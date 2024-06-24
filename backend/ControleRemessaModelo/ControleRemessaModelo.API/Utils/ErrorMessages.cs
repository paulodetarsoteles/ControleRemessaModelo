using ControleRemessaModelo.API.Enums;

namespace ControleRemessaModelo.API.Utils
{
    public static class ErrorMessages
    {
        public static string GetMessage(ErrorCode errorCode)
        {
            return _errorMessages.TryGetValue(errorCode, out var message) ? message : "Erro desconhecido.";
        }

        //MENSAGENS
        private static readonly Dictionary<ErrorCode, string> _errorMessages = new Dictionary<ErrorCode, string>
        {
            //Autenticação
            { ErrorCode.None, "None" },
            { ErrorCode.UsuarioNaoEncontrado, "Usuário não encontrado." },
            { ErrorCode.UsuarioOuSenhaInvalida, "Usuário ou senha inválida." },
            { ErrorCode.UsuarioBloqueado, "Usuário Bloqueado." },
            { ErrorCode.UsuarioComPendencia, "Usuário com pendência." },
            { ErrorCode.ProblemaNoServicoAutenticacao, "Problema no serviço de autenticação." },

            //Arquivo de configuração
            { ErrorCode.ErroAoBuscarChaveNaConfiguracao, "Chave secreta não encontrada na configuração." },
            { ErrorCode.ProblemaAoBuscarKeyNaConfig, "Problema ao buscar chave secreta na configuração." },
            { ErrorCode.ErroAoBuscarValorDaKey, "Problema ao pegar valor da chave." },
            { ErrorCode.ErroAoConverterValorDaConfig, "Erro ao converter valor da chave de expiração." }
        };

        public static int RetornarStatucCode(ErrorCode ErroAutenticacao)
        {
            int resultado = 0;

            switch (ErroAutenticacao)
            {
                case ErrorCode.UsuarioNaoEncontrado:
                    resultado = 404;
                    break;

                case ErrorCode.UsuarioOuSenhaInvalida:
                case ErrorCode.UsuarioBloqueado:
                case ErrorCode.UsuarioComPendencia:
                    resultado = 401;
                    break;

                case ErrorCode.ProblemaNoServicoAutenticacao:
                case ErrorCode.ErroAoBuscarChaveNaConfiguracao:
                case ErrorCode.ProblemaAoBuscarKeyNaConfig:
                case ErrorCode.ErroAoBuscarValorDaKey:
                case ErrorCode.ErroAoConverterValorDaConfig:
                    resultado = 500;
                    break;

                case ErrorCode.None:
                    break;
                default:
                    break;
            }

            return resultado;
        }
    }
}
