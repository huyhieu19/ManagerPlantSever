using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ManagerServer.Common
{
    public class GetIdByToken
    {
        private readonly IConfiguration configuration;
        public GetIdByToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetIdbyToken(string token)
        {
            // Khóa bí mật đối xứng
            string secretKey = configuration["JWT:Secret"];
            var symmetricKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (secretKey));

            // Xác định các thông tin cần thiết cho việc giải mã
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"],
                IssuerSigningKey = symmetricKey
            };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var claimsPrincipal = tokenHandler.ValidateToken (token, tokenValidationParameters, out var validatedToken);

            // Lấy thông tin từ token
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.FirstOrDefault (c => c.Type == "Id")?.Value;
            return userId;
        }
    }
}
