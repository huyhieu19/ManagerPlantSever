using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Authr;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ManagerServer.Service.VisitorServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(SignInRequestModel model)
        {
            var result = await signInManager.PasswordSignInAsync (
               model.Email, model.Password, false, false);
            if ( !result.Succeeded )
            {
                return "";
            }
            var authClaims = await GetAuthClaims (await userManager.FindByEmailAsync (model.Email));
            var token = GenarateToken (authClaims);
            return await Task.FromResult (token);
        }

        public async Task<(int, string)> SignUpAsync(SignUpRequestModel model)
        {
            var user = new AppUser
            {
                FirstName = model.FistName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Birthday = DateTime.Now,
                PhoneNumber = model.NumberPhone,
            };
            var userExits = await userManager.FindByEmailAsync (user.Email);
            if ( userExits != null )
            {
                return (-2, "");
            }
            var result = await userManager.CreateAsync (user, model.Password);
            if ( result.Succeeded )
            {
                //add role admin tam thoi de dung het chuc nang
                var userAdd = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(userAdd, "Admin");

                var tempUser = await userManager.FindByEmailAsync (model.Email);
                var authClaims = await GetAuthClaims (tempUser);
                var token = GenarateToken (authClaims);
                return await Task.FromResult ((0, token));
            }
            return (-1, "");
        }
        private async Task<List<Claim>> GetAuthClaims(IdentityUser user)
        {

            var authClaims = new List<Claim>
             {
                 new Claim("Id", user.Id),
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) ,

             };
            var roles = await userManager.GetRolesAsync (await userManager.FindByEmailAsync (user.Email));
            if ( roles.Count > 0 )
                foreach ( var role in roles )
                {
                    //add role in token
                    authClaims.Add (new Claim (ClaimTypes.Role, role));

                    //add role claim in token
                    var clams = await roleManager.GetClaimsAsync (new IdentityRole (role));
                    if ( clams.Count > 0 )
                    {
                        foreach ( var claim in clams )
                        {
                            authClaims.Add (claim);
                        }
                    }
                }

            return authClaims;
        }
        private string GenarateToken(IEnumerable<Claim> authClaims)
        {
            var authenKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (configuration["JWT:Secret"]!));
            var tokenObject = new JwtSecurityToken (
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes (20),
                claims: authClaims,
                signingCredentials: new SigningCredentials (authenKey, SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler ().WriteToken (tokenObject);
        }

        public async Task<AppUser> Getinfo(AutherRequest request)
        {

            // Khóa bí mật đối xứng
            string secretKey = configuration["JWT:Secret"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

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

            try
            {
                // Giải mã và xác minh tính hợp lệ của token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(request.Token!, tokenValidationParameters, out var validatedToken);

                // Lấy thông tin từ token
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                
                var user = await userManager.FindByIdAsync (userId);

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
