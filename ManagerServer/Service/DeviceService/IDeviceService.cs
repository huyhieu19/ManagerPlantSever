using ManagerServer.Database.Entity;
using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;

namespace ManagerServer.Service.DeviceService
{
    public interface IDeviceService
    {
         Task<List<DeviceEntity>> GetAllDevice();
         Task<DeviceEntity> GetById(DeviceRequestModel requestModel);
         Task<ResponseModel<bool>> SetDeviceToZone(DeviceRequestModel requestModel);
         Task<ResponseModel<List<DeviceEntity>>> GetDeviceAtive(DeviceRequestModel requestModel);
         Task<ResponseModel<List<DeviceEntity>>> GetDeviceByZoneId(DeviceRequestModel requestModel);
    }
}
