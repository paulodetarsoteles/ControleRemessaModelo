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
                UsuarioDTO usuario = usuarioServico.GetUsuarioLogin(user.UserName);

                if (usuario is null)
                    return ErrorMessages.UsuarioNaoEncontrado;

                if (usuario.UserName != user.UserName && usuario.Password != user.Password)
                    return ErrorMessages.UsuarioOuSenhaInvalida;

                string keySecret = "SecretKey";
                char[] valueSecret = ConfigurationFile.GetConfigurationKey(keySecret);

                if (valueSecret is null)
                    return ErrorMessages.ErroAoBuscarValorDaKey;

                SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(valueSecret));

                string keyExpires = "ExpiresTokenInHour";
                char[] valueExpires = ConfigurationFile.GetConfigurationKey(keyExpires);

                if (valueExpires is null)
                    return ErrorMessages.ErroAoBuscarValorDaKey;

                if (!int.TryParse(valueExpires, out int expiresIn))
                    return ErrorMessages.ErroAoConverterValorDaConfig;

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

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
