using ManagerServer.Model.DataDeviceModel;
using ManagerServer.Service.DataDeviceService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DataDeviceController : ControllerBase
    {
        private readonly IDataDeviceService dataDeviceService;

        public DataDeviceController(IDataDeviceService dataDeviceService)
        {
            this.dataDeviceService = dataDeviceService;
        }
        [HttpPost("getdeviceidbyshid")]
        public async Task<int> GetDeviceIdByShId([FromBody] DataDeviceQueryModel queryModel)
        {
            return await dataDeviceService.GetDataDeviceId(queryModel);
        }
    }
}
