using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Zone;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.ZoneService
{
    public class ZoneService : IZoneService
    {
        private readonly ManagerDbContext dbContext;

        public ZoneService(ManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateZone(ZoneQueryModel queryModel)
        {
            var zone = new ZoneEntity()
            {
                Name = queryModel.ZoneName,
                Decription = queryModel.Decription,
                Image = queryModel.Image,
                FarmId = queryModel.FarmId,
                UpdateAt = DateTime.Now
            };
            await dbContext.AddAsync(zone);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<ZoneEntity>> GetAllZones()
        {
            var result = await dbContext.ZoneEntities.ToListAsync();
            return result;
        }

        public async Task<ZoneEntity> GetZoneById(ZoneQueryModel queryModel)
        {
            var result = await dbContext.ZoneEntities.FirstOrDefaultAsync(p => p.Id == queryModel.Id);
            return result;
        }

        public async Task<bool> UpdateZone(ZoneQueryModel queryModel)
        {
            var zone = await dbContext.ZoneEntities.FirstOrDefaultAsync(p=>p.Id ==queryModel.Id);
            if( zone!= null)
            {
                if(queryModel.ZoneName!=null) zone.Name = queryModel.ZoneName;
                if(queryModel.Decription!=null) zone.Decription = queryModel.Decription;
                if(queryModel.Image!=null) zone.Image = queryModel.Image;
                if(queryModel.FarmId!=null) zone.FarmId = queryModel.FarmId;
            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
