using AutoMapper;
using ControleRemessaModelo.Entidades.Models;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Repositorio.Interfaces;

namespace ControleRemessaModelo.Negocio.Servicos
{
    public class RoleServico : IRoleServico
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepositorio _roleRepositorio;

        public RoleServico(
            IMapper mapper,
            IRoleRepositorio rolerepositorio
            )
        {
            _mapper = mapper;
            _roleRepositorio = rolerepositorio;
        }

        public List<RoleDTO>? Buscar()
        {
            try
            {
                List<Role>? roles = _roleRepositorio.Buscar();

                if (roles is null || roles.Count == 0)
                {
                    return null;
                }

                List<RoleDTO> resultado = [];

                foreach (Role role in roles)
                {
                    RoleDTO roleDTO = _mapper.Map<RoleDTO>(role);
                    resultado.Add(roleDTO);
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
