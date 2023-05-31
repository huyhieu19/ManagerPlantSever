using ManagerServer.Database.Entity;
using ManagerServer.Service.DeviceService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService service;

        public DeviceController(IDeviceService service)
        {
            this.service = service;
        }
        [HttpGet, Route("get-all")]
        public async Task<List<DeviceEntity>> GetAllDevice()
        {
            return await service.GetAllDevice();
        }


    }
}
