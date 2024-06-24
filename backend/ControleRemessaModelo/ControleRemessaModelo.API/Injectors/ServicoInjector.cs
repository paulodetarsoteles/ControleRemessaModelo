using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Negocio.Servicos;

namespace ControleRemessaModelo.API.Injectors
{
    public static class ServicoInjector
    {
        public static void Injector(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsuarioServico, UsuarioServicos>();
        }
    }
}
