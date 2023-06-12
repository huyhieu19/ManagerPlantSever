using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;

namespace ManagerServer.Service.DeviceActionService
{
    public interface IDeviceActionService
    {
        Task<ResponseModel<List<DeviceActionDisplayModel>>> GetDeviceActionByZoneId(int zoneId);
        Task<ResponseModel<bool>> DeleteDeviceAction(RemoveDeviceActionModel requestModel);
        Task<ResponseModel<bool>> UpdateDeviceAction(DeviceActionUpdateModel updateModel);
        Task<ResponseModel<bool>> TurnOnDeviceAction(RemoveDeviceActionModel requestModel);
        Task<ResponseModel<bool>> TurnOffDeviceAction(RemoveDeviceActionModel requestModel);

    }
}
