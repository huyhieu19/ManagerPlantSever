using Azure.Core;
using Common.Model.Farm;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
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
        public async Task<ResponseModel<bool>> AddFarm(FarmQueryModel queryModel)
        {
            var ownerId = GetIdbyToken(queryModel.Token);
            
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
                    code = 0,
                    message = "Success",
                    data = true,
                } : new ResponseModel<bool> ()
                {
                    code = -1,
                    message = "Error",
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

        public async Task<List<FarmEntity>> GetByOwnerId(TokenRequestBase tokenRequest)
        {
           
            try
            {
                string userId = this.GetIdbyToken(tokenRequest.Token);
                var query = from data in dbContext.FarmEntities
                            where data.OwnerId == userId
                            select data;
                return await query.ToListAsync ();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFarm(FarmUpdateModel query)
        {
            var farm = await dbContext.FarmEntities.FindAsync (query.Id);
            if ( farm != null )
            {
                if ( query.FarmName != null ) farm.Name = query.FarmName;
                if ( query.Adress != null ) farm.Adress = query.Adress;
                if ( query.Avata != null ) farm.Avata = query.Avata;
                if ( query.OwnerID != null ) farm.OwnerId = query.OwnerID;
                farm.UpdateAt = DateTime.Now;
            }

            return await dbContext.SaveChangesAsync () > 0;
        }

        public string GetIdbyToken(string token)
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            // Lấy thông tin từ token
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return userId;
        }

    }

}
