using Common.Model.Farm;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Farm;
using ManagerServer.Model.ResponeModel;
using ManagerServer.Service.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ManagerServer.Service.FarmService
{
    public class FarmService : IFarmService
    {
        private readonly ManagerDbContext dbContext;
        private readonly IConfiguration configuration;

        public FarmService(ManagerDbContext dbContext, IUserService userService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        /// <summary>
        /// Use to create farm, only owner can create
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel<bool>> AddFarm(FarmQueryModel queryModel, string token)
        {
            var ownerId = this.GetIdbyToken (token);

            var farm = new FarmEntity ()
            {
                Name = queryModel.FarmName,
                Decription = queryModel.FarmDescription,
                Adress = queryModel.Adress,
                Avata = queryModel.Avata,
                OwnerId = ownerId,
            };
            dbContext.Add (farm);
            var result = await dbContext.SaveChangesAsync ();
            return result > 0 ? new ResponseModel<bool> ()
            {
                code = 1,
                message = "Success adding one farm",
                data = true,
            } : new ResponseModel<bool> ()
            {
                code = 0,
                message = "Error adding one farm",
                data = false,
            };

        }

        public async Task<List<FarmEntity>> GetAll()
        {
            return await dbContext.FarmEntities.ToListAsync ();
        }

        public async Task<FarmEntity> GetById(int Id)
        {
            FarmEntity? Result = await dbContext.FarmEntities.FirstOrDefaultAsync (p => p.Id == Id);
            if ( Result == null )
            {
                return new FarmEntity ();
            }
            return Result;
        }
        /// <summary>
        /// Get By Onnwer, after get token on header, get OwnerId by Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<FarmEntity>>> GetByOwnerId(string token)
        {

            try
            {
                string userId = this.GetIdbyToken (token);
                var farms = await dbContext.FarmEntities.Where (p => p.OwnerId.Equals (userId)).ToListAsync ();
                return new ResponseModel<List<FarmEntity>> ()
                {
                    code = 1,
                    message = "Get farm by owner success",
                    data = farms,
                };
            }
            catch ( Exception ex )
            {
                return new ResponseModel<List<FarmEntity>> ()
                {
                    code = 0,
                    message = "Get farm by owner fall"
                };
                throw;
            }
        }
        /// <summary>
        /// use for update farm
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFarm(FarmUpdateModel query, string token)
        {
            try
            {
                var farm = await dbContext.FarmEntities.FindAsync (query.Id);
                if ( farm != null )
                {
                    if ( query.FarmName != null ) farm.Name = query.FarmName;
                    farm.Adress = query.Adress;
                    farm.Avata = query.Avata;
                    farm.UpdateAt = DateTime.Now;
                }
                return await dbContext.SaveChangesAsync () > 0;
            }
            catch
            {
                return false;
                throw;
            }
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
