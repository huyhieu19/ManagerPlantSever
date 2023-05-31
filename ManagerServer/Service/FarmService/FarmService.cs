using Common.Model.Farm;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.FarmService
{
    public class FarmService : IFarmService
    {
        private readonly ManagerDbContext dbContext;

        public FarmService(ManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddFarm(FarmQueryModel queryModel)
        {
            var farm = new FarmEntity()
            {
                Name = queryModel.FarmName,
                Decription = queryModel.FarmDescription,
                Adress = queryModel.Adress,
                Avata = queryModel.Avata,
                OwnerId = queryModel.OwnerID,
            };
            dbContext.Add(farm);
            var result = await dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<FarmEntity>> GetAll()
        {
            return await dbContext.FarmEntities.ToListAsync();
        }

        public async Task<FarmEntity> GetById(FarmQueryModel queryModel)
        {
            return await dbContext.FarmEntities.FirstOrDefaultAsync(p=>p.Id == queryModel.Id);
        }

        public async Task<bool> UpdateFarm(FarmQueryModel query)
        {
            var farm = await dbContext.FarmEntities.FindAsync(query.Id);
            if (farm != null)
            {
                if(query.FarmName!=null) farm.Name = query.FarmName;
                if (query.Adress != null) farm.Adress = query.Adress;
                if (query.Avata != null) farm.Avata = query.Avata;
                if (query.OwnerID != null) farm.OwnerId = query.OwnerID;
                farm.UpdateAt = DateTime.Now;
            }

            return await dbContext.SaveChangesAsync() >0;
        }
    }
}
