using Common.Model.SlaveModel;
using ManagerServer.Service.SlaveServices;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SlaveController: ControllerBase
    {
        private readonly ISlaveService slaveService;

        public SlaveController(ISlaveService slaveService)
        {
            this.slaveService = slaveService;
        }
        [HttpPost("getrealtime")]
        public async Task<List<SlaveDisplayDataModel>> ShowDataRealTime([FromBody] SlaveQueryDataModel query)
        {
            return await slaveService.GetDataRealTime(query);
        }

    }
}
