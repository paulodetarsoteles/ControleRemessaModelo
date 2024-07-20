using ControleRemessaModelo.API.Enums;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.DTOs;
using ControleRemessaModelo.Negocio.Interfaces;
using ControleRemessaModelo.Negocio.Responses;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ControleRemessaModelo.API.Services
{
    public class AutenticacaoUsuarioJWT(IUsuarioServico usuarioServico, ILogger<AutenticacaoUsuarioJWT> _logger)
    {
        public DefaultResponseAPI GenerateToken(UsuarioLoginDTO user)
        {
            DefaultResponseAPI ret = new();

            try
            {
                UsuarioDTO? usuario = usuarioServico.GetUsuarioLogin(user.Login);

                if (usuario == null || usuario.Login != user.Login || usuario.Senha != user.Senha)
                {
                    ret.Success = false;
                    ret.BodyResponse = [];

                    ErrorMessageResponseAPI error = new();

                    if (usuario == null)
                    {
                        error.ErrorMessage = ErrorMessages.GetMessage(ErrorCode.UsuarioNaoEncontrado);
                        ret.StatusCode = StatusCodes.Status204NoContent;
                    }
                    else
                    {
                        error.ErrorMessage = ErrorMessages.GetMessage(ErrorCode.UsuarioOuSenhaInvalida);
                        ret.StatusCode = StatusCodes.Status401Unauthorized;
                    }

                    ret.Errors.Add(error);

                    return ret;
                }

                string keySecret = "SecretKey";

                char[] valueSecret = ConfigurationFile.GetConfigurationKey(keySecret) ??
                    throw new Exception(ErrorMessages.GetMessage(ErrorCode.ErroAoBuscarValorDaKey));

                SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(valueSecret));

                string keyExpires = "ExpiresTokenInHour";

                char[] valueExpires = ConfigurationFile.GetConfigurationKey(keyExpires) ??
                    throw new Exception(ErrorMessages.GetMessage(ErrorCode.ErroAoBuscarValorDaKey));

                if (!int.TryParse(valueExpires, out int expiresIn))
                {
                    throw new Exception(ErrorMessages.GetMessage(ErrorCode.ErroAoConverterValorDaConfig));
                }

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
                {
                    throw new Exception(ErrorMessages.GetMessage(ErrorCode.ProblemaNoServicoAutenticacao));
                }

                ret.Success = true;
                ret.Errors = [];
                ret.StatusCode = StatusCodes.Status200OK;
                ret.BodyResponse = [token];
            }
            catch (Exception ex)
            {
                _logger.LogError($"{TypeDescriptor.GetClassName(this)} - {nameof(MethodBase)} - {ex.Message} - {ex.StackTrace}", "ERRO");

                ErrorMessageResponseAPI error = new()
                {
                    ErrorMessage = ErrorMessages.GetMessage(ErrorCode.ErroInesperado)
                };

                ret.Success = false;
                ret.StatusCode = StatusCodes.Status500InternalServerError;
                ret.BodyResponse = [];
            }

            return ret;
        }

        public string GetHashMd5(string input)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
