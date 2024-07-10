using ControleRemessaModelo.API.Enums;
using ControleRemessaModelo.API.Excecoes;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ControleRemessaModelo.API.Services
{
    public class AutenticacaoUsuarioJWT(IUsuarioServico usuarioServico) : IAutenticacaoUsuarioJWT
    {
        public string GenerateToken(UsuarioLoginDTO user)
        {
            try
            {
                UsuarioDTO? usuario = usuarioServico.GetUsuarioLogin(user.Login) ??
                    throw new CustomException(ErrorCode.UsuarioNaoEncontrado);

                if (usuario.Login != user.Login || usuario.Senha != user.Senha)
                    throw new CustomException(ErrorCode.UsuarioOuSenhaInvalida);

                string keySecret = "SecretKey";

                char[] valueSecret = ConfigurationFile.GetConfigurationKey(keySecret) ??
                    throw new CustomException(ErrorCode.ErroAoBuscarValorDaKey);

                SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(valueSecret));

                string keyExpires = "ExpiresTokenInHour";

                char[] valueExpires = ConfigurationFile.GetConfigurationKey(keyExpires) ??
                    throw new CustomException(ErrorCode.ErroAoBuscarValorDaKey);

                if (!int.TryParse(valueExpires, out int expiresIn))
                    throw new CustomException(ErrorCode.ErroAoConverterValorDaConfig);

                SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokenOptions = new
                (
                    claims:
                    [
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.Role.Nome)
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

        public string GetHashMd5(string input)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }
    }
}
