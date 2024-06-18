using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleRemessaModelo.API.Services
{
    public class AutenticacaoUsuarioJWT
    {
        private readonly static IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static char[] Secret()
        {
            string secretKey = configuration["SecretKey"] ??
                throw new Exception("Chave sereta não encontrada no arquivo de configuração.");

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("Problema ao buscar chave secreta na configuração");

            return secretKey.ToCharArray();
        }

        internal static string GenerateJwtToken(string username)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            byte[]? key = Encoding.ASCII.GetBytes(Secret());

            SecurityTokenDescriptor? tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Name, username)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
