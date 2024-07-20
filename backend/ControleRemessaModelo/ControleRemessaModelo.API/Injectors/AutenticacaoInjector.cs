using ControleRemessaModelo.API.Services;

namespace ControleRemessaModelo.API.Injectors
{
    public static class AutenticacaoInjector
    {
        public static void Injector(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AutenticacaoUsuarioJWT>();
        }
    }
}
