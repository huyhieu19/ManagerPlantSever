using ManagerServer.Database.Entity;

namespace ManagerServer.Service.DeviceService
{
    public interface IDeviceService
    {
        public Task<List<DeviceEntity>> GetAllDevice();
        public Task<DeviceEntity> GetById(int id);
        public Task<bool> SetDeviceToZone(int zoneId, int DeviceId);
    }
}
