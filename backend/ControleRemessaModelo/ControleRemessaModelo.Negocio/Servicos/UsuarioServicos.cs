using AutoMapper;
using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Negocio.Responses;
using ControleRemessaModelo.Repositorio.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace ControleRemessaModelo.Negocio.Servicos
{
    public class UsuarioServicos : IUsuarioServico
    {
        private readonly ILogger<UsuarioServicos> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRoleRepositorio _roleRepositorio;

        public UsuarioServicos(
            ILogger<UsuarioServicos> logger,
            IMapper mapper,
            IUsuarioRepositorio usuarioRepositorio,
            IRoleRepositorio rolerepositorio
            )
        {
            _logger = logger;
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
            _roleRepositorio = rolerepositorio;
        }

        public UsuarioDTO? GetUsuarioLogin(string userName)
        {
            try
            {
                Usuario? usuario = _usuarioRepositorio.BuscarPorLogin(userName);

                if (usuario is null)
                {
                    return null;
                }

                List<Role>? roles = _roleRepositorio.Buscar();

                if (roles is null || roles.Count == 0)
                {
                    return null;
                }

                Role role = roles.First(r => r.Id == usuario.Id);

                UsuarioDTO resultado = _mapper.Map<UsuarioDTO>(usuario);
                resultado.Role = role;

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DefaultResponseAPI Buscar()
        {
            DefaultResponseAPI resultato = new();

            try
            {
                List<Usuario>? usuarios = _usuarioRepositorio.Buscar();

                if (usuarios is null)
                {
                    resultato.StatusCode = (int)HttpStatusCode.NoContent;
                    resultato.Success = true;

                    return resultato;
                }

                List<Role>? roles = _roleRepositorio.Buscar();

                List<UsuarioDTO> usuariosDTO = [];

                foreach (var usuario in usuarios)
                {
                    UsuarioDTO usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

                    if (roles is not null)
                    {
                        usuarioDTO.Role = roles.First(r => r.Id == usuarioDTO.Id);
                    }

                    usuariosDTO.Add(usuarioDTO);
                }

                resultato.BodyResponse.AddRange(usuariosDTO);
                resultato.StatusCode = (int)HttpStatusCode.OK;
                resultato.Success = true;

                return resultato;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{TypeDescriptor.GetClassName(this)} - {nameof(MethodBase)} - {ex.Message} - {ex.StackTrace}", "ERRO");

                resultato.Errors.Add(new()
                {
                    ErrorMessage = "Ocorreu um erro inesperado"
                });

                return resultato;
            }
        }
    }
}
