using ControleRemessaModelo.API.Enums;
using ControleRemessaModelo.API.Excecoes;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleRemessaModelo.API.Services
{
    public class AutenticacaoUsuarioJWT(IUsuarioServico usuarioServico) : IAutenticacaoUsuarioJWT
    {
        public string GenerateToken(UsuarioLoginDTO user)
        {
            try
            {
                UsuarioDTO? usuario = usuarioServico.GetUsuarioLogin(user.UserName);

                if (usuario is null)
                    throw new CustomException(ErrorCode.UsuarioNaoEncontrado);

                if (usuario.UserName != user.UserName || usuario.Password != user.Password)
                    throw new CustomException(ErrorCode.UsuarioOuSenhaInvalida);

                string keySecret = "SecretKey";
                char[] valueSecret = ConfigurationFile.GetConfigurationKey(keySecret);

                if (valueSecret is null)
                    throw new CustomException(ErrorCode.ErroAoBuscarValorDaKey);

                SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(valueSecret));

                string keyExpires = "ExpiresTokenInHour";
                char[] valueExpires = ConfigurationFile.GetConfigurationKey(keyExpires);

                if (valueExpires is null)
                    throw new CustomException(ErrorCode.ErroAoBuscarValorDaKey);

                if (!int.TryParse(valueExpires, out int expiresIn))
                    throw new CustomException(ErrorCode.ErroAoConverterValorDaConfig);

                SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokenOptions = new
                (
                    claims:
                    [
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.Role)
                    ],
                    expires: DateTime.Now.AddHours(expiresIn),
                    signingCredentials: signingCredentials
                );

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                if (string.IsNullOrEmpty(token))
                    throw new CustomException(ErrorCode.ProblemaNoServicoAutenticacao);

                return token;
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
