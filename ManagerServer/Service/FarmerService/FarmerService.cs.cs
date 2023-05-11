using Common.Mapper;
using Common.Model.Farm;
using ManagerServer.Common.Mapper;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Owner;
using ManagerServer.Service.FarmerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.FarmerService
{
    public class FarmarService: IFarmerService
    {
        private readonly ManagerDbContext context;
        private readonly ILogger<FarmarService> logger;

        public FarmarService(ManagerDbContext context, ILogger<FarmarService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> AddFarm(FarmQueryModel queryModel)
        {
            var data = new FarmEntity()
            {
                Decription = queryModel.FarmDescription,
                Name = queryModel.FarmName,
                OwnerId = queryModel.OwnerID
            };
            await context.AddAsync(data);
            var result = await context.SaveChangesAsync()> 0 ? true:false ;
            return  result;
        }      
        public Task<bool> DeleteFarm(FarmQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditFarm(FarmQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingReponseModel<FarmDisplayModel>> GetAllFarm(FarmQueryModel queryModel)
        {
          
               var reponse = new PagingReponseModel<FarmDisplayModel>()
               {
                   Data = from data in await context.FarmEntities
                          .Skip((queryModel.PageNumber.Value-1)*queryModel.PageSize.Value)
                          .Take(queryModel.PageSize.Value).ToListAsync()
                          select data.ToFarmModel(),
                   PageSize = queryModel.PageSize.Value,
                   PageNumber = queryModel.PageNumber.Value,
                   Total = await context.FarmEntities.CountAsync()
               };
            return reponse;
               
          
        }

        public async Task<FarmDisplayModel> GetFarmById(FarmQueryModel queryModel)
        {
            var reponse = from data in await context.FarmEntities.ToListAsync()
                          where data.Id == queryModel.Id
                          select data.ToFarmModel();
            return reponse.FirstOrDefault();
        }

        public async Task<PagingReponseModel<FarmDisplayModel>> GetFarmByOwnerId(FarmQueryModel queryModel)
        {
            var query = from o in context.Users
                           join f in context.FarmEntities on o.Id equals f.OwnerId
                           where f.OwnerId == queryModel.OwnerID
                           select f;
            var reponse = new PagingReponseModel<FarmDisplayModel>()
            {
                Data = from data in await query
                       .Skip((queryModel.PageNumber.Value - 1) * queryModel.PageSize.Value)
                       .Take(queryModel.PageSize.Value).ToListAsync()
                       select data.ToFarmModel(),
                PageSize = queryModel.PageSize.Value,
                PageNumber = queryModel.PageNumber.Value,
                Total = await query.CountAsync()
            };
            return reponse;
        }

        public async Task<OwnerDisplayModel> GetOwner(FarmQueryModel queryModel)
        {
            var result =await context.Users.FindAsync(queryModel.OwnerID);
            return result.ToOwnerModel();
        }
    }
}
