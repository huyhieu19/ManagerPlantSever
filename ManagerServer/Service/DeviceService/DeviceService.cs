using ManagerServer.Database;
using ManagerServer.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly ManagerDbContext dbContext;

        public DeviceService(ManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<DeviceEntity>> GetAllDevice()
        {
            var result = await dbContext.DeviceEntities.ToListAsync();
            return result;
        }

        public async Task<DeviceEntity> GetById(int id)
        {
            return await dbContext.DeviceEntities.FirstOrDefaultAsync(p=>p.Id ==id);
        }

        public async Task<bool> SetDeviceToZone(int zoneId, int DeviceId)
        {
            var device = await dbContext.DeviceEntities.FirstOrDefaultAsync(p => p.Id == DeviceId);
            if (device != null)
            {
                device.ZoneId = zoneId;
            };
            return true;
        }
    }
}
