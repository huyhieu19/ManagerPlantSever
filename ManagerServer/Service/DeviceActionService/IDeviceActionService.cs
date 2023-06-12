using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;

namespace ManagerServer.Service.DeviceActionService
{
    public interface IDeviceActionService
    {
        Task<ResponseModel<List<DeviceActionDisplayModel>>> GetDeviceActionByZoneId(int zoneId);
        Task<DeviceActionDisplayModel> DeleteDeviceAction(int zoneId);
        Task<DeviceActionDisplayModel> UpdateDeviceAction(int zoneId);
    }
}
