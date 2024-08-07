﻿using AutoMapper;
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
        private readonly IRoleRepositorio _roleRepositorio;

        public UsuarioServicos(
            IMapper mapper,
            IUsuarioRepositorio usuarioRepositorio,
            IRoleRepositorio rolerepositorio
            )
        {
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
                    return null;

                List<Role>? roles = _roleRepositorio.Buscar();

                if (roles is null || roles.Count == 0)
                    return null;

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
    }
}
