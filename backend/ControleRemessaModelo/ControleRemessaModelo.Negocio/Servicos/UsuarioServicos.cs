using AutoMapper;
using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Repositorio.Interfaces;

namespace ControleRemessaModelo.Negocio.Servicos
{
    public class UsuarioServicos : IUsuarioServico
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServicos(IMapper mapper, IUsuarioRepositorio usuarioRepositorio)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public UsuarioDTO? GetUsuarioLogin(string userName)
        {
            try
            {
                Usuario? usuario = _usuarioRepositorio.GetUsuarioLogin(userName);

                if (usuario is null)
                    return null;

                UsuarioDTO resultado = _mapper.Map<UsuarioDTO>(usuario);

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
