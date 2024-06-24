using AutoMapper;
using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Negocio.DTOs;

namespace ControleRemessaModelo.Negocio.Helpers
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}
