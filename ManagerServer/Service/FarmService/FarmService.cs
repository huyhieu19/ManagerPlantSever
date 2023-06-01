using Common.Model.Farm;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Farm;
using ManagerServer.Model.ResponeModel;
using ManagerServer.Service.UserService;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.FarmService
{
    public class FarmService : IFarmService
    {
        private readonly ManagerDbContext dbContext;
        private readonly IUserService userService;
        public FarmService(ManagerDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }
        public async Task<ResponseModel<bool>> AddFarm(FarmQueryModel queryModel)
        {
            var owner = await userService.GetById (queryModel.OwnerID!);
            if ( owner == null )
            {
                return new ResponseModel<bool> ()
                {
                    code = 1,
                    message = "User notFound",
                    data = false,
                };
            }
            else
            {
                var farm = new FarmEntity ()
                {
                    Owner = owner,
                    Name = queryModel.FarmName,
                    Decription = queryModel.FarmDescription,
                    Adress = queryModel.Adress,
                    Avata = queryModel.Avata,
                    OwnerId = queryModel.OwnerID,
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
    }
}
