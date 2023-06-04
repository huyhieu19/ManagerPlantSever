using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Device;
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

        public async Task<DeviceEntity> GetById(DeviceRequestModel requestModel)
        {
            return await dbContext.DeviceEntities.FirstOrDefaultAsync(p=>p.Id ==requestModel.DeviceId);
        }

        public async Task<bool> SetDeviceToZone(DeviceRequestModel requestModel)
        {
            var device = await dbContext.DeviceEntities.FirstOrDefaultAsync(p => p.Id == requestModel.DeviceId);
            if (device != null)
            {
                device.ZoneId = requestModel.ZoneId;
            };
            return true;
        }
    }
}
