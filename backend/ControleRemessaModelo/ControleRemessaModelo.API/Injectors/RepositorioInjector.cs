using ControleRemessaModelo.Repositorio.Interfaces;
using ControleRemessaModelo.Repositorio.Repositorios;

namespace ControleRemessaModelo.API.Injectors
{
    public static class RepositorioInjector
    {
        public static void Injector(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IRoleRepositorio, RoleRepositorio>();
        }
    }
}
