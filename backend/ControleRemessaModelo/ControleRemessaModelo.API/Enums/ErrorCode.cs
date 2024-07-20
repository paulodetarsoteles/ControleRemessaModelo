namespace ControleRemessaModelo.API.Enums
{
    public enum ErrorCode
    {
        ErroInesperado = 0,
        UsuarioNaoEncontrado = 1,
        UsuarioOuSenhaInvalida = 2,
        UsuarioBloqueado = 3,
        UsuarioComPendencia = 4,
        ProblemaNoServicoAutenticacao = 5,
        ErroAoBuscarChaveNaConfiguracao = 6,
        ProblemaAoBuscarKeyNaConfig = 7,
        ErroAoBuscarValorDaKey = 8,
        ErroAoConverterValorDaConfig = 9
    }
}
