using ManagerServer.Database.Entity;
using ManagerServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ManagerServer.Service.VisitorServices
{
    public class VisitorService : IVisitorService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public VisitorService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<(string, string)> SignInAsync(SignInRequestModel model)
        {
            var result = await signInManager.PasswordSignInAsync(
               model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return (string.Empty, string.Empty);
            }
            var authClaims = await GetAuthClaims(await userManager.FindByEmailAsync(model.Email));

            var token = GenarateToken(authClaims);
            var id = await userManager.GetUserIdAsync(await userManager.FindByEmailAsync(model.Email));
            return await Task.FromResult((token, id));
        }

        public async Task<(string, string)> SignUpAsync(SignUpRequestModel model)
        {
            var user = new AppUser
            {
                FirstName = model.FistName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Dob = DateTime.Now,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var tempUser = await userManager.FindByEmailAsync(model.Email);
                var id = await userManager.GetUserIdAsync(tempUser);
                var authClaims = await GetAuthClaims(tempUser);
                var token = GenarateToken(authClaims);
                return await Task.FromResult((token, id));
            }
            else return (string.Empty, string.Empty);
        }
        private async Task<List<Claim>> GetAuthClaims(IdentityUser user)
        {

            var authClaims = new List<Claim>
             {
                 new Claim("Id", user.Id),
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) ,

             };
            var roles = await userManager.GetRolesAsync(await userManager.FindByEmailAsync(user.Email));
            if (roles.Count > 0)
                foreach (var role in roles)
                {
                    //add role in token
                    authClaims.Add(new Claim(ClaimTypes.Role, role));

                    //add role claim in token
                    var clams = await roleManager.GetClaimsAsync(new IdentityRole(role));
                    if (clams.Count > 0)
                    {
                        foreach (var claim in clams)
                        {
                            authClaims.Add(claim);
                        }
                    }
                }

            return authClaims;
        }
        private string GenarateToken(IEnumerable<Claim> authClaims)
        {
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
