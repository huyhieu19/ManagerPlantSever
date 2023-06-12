using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;
using ManagerServer.Service.DeviceActionService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    public class DeviceActionController : Controller
    {
        private readonly IDeviceActionService deviceActionService;

        public DeviceActionController(IDeviceActionService deviceActionService)
        {
            this.deviceActionService = deviceActionService;
        }
        [HttpDelete, Route ("deviceAction")]
        public Task<ResponseModel<bool>> DeleteDeviceAction([FromBody] RemoveDeviceActionModel requestModel)
        {
            return this.deviceActionService.DeleteDeviceAction (requestModel);
        }
        [HttpGet, Route ("deviceActions")]
        public Task<ResponseModel<List<DeviceActionDisplayModel>>> GetDeviceActionByZoneId(int zoneId)
        {
            return this.deviceActionService.GetDeviceActionByZoneId (zoneId);
        }
        [HttpPut, Route ("deviceAction")]
        public Task<ResponseModel<bool>> UpdateDeviceAction([FromBody] DeviceActionUpdateModel updateModel)
        {
            return this.deviceActionService.UpdateDeviceAction (updateModel);
        }
        [HttpPost, Route ("TurnOndeviceAction")]
        public Task<ResponseModel<bool>> TurnOnDeviceAction([FromBody] RemoveDeviceActionModel requestModel)
        {
            return this.deviceActionService.TurnOnDeviceAction (requestModel);
        }
        [HttpPost, Route ("TurnOffdeviceAction")]
        public Task<ResponseModel<bool>> TurnOffDeviceAction([FromBody] RemoveDeviceActionModel requestModel)
        {
            return this.deviceActionService.TurnOffDeviceAction (requestModel);
        }
    }
}
