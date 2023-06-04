using ManagerServer.Database.Entity;
using ManagerServer.Model.Device;

namespace ManagerServer.Service.DeviceService
{
    public interface IDeviceService
    {
        public Task<List<DeviceEntity>> GetAllDevice();
        public Task<DeviceEntity> GetById(DeviceRequestModel requestModel);
        public Task<bool> SetDeviceToZone(DeviceRequestModel requestModel);
    }
}
