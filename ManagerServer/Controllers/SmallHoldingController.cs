using ManagerServer.Model.SMH;
using ManagerServer.Service.SmallHoldingServices;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SmallHoldingController:ControllerBase
    {
        private readonly ISmallHoldingService service;

        public SmallHoldingController(ISmallHoldingService service)
        {
            this.service = service;
        }

        [HttpPost("getshpaging")]
        public async Task<IActionResult> GetSHPaging([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetAllSHM(queryModel);
            return Ok(result);
        }
        [HttpPost("getshbyid")]
        public async Task<IActionResult> GetSHbyId([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetSMHById(queryModel);
            return Ok(result);
        }
        [HttpPost("getshbyfarmid")]
        public async Task<IActionResult> GetSHbyFarm([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetSMHByFarmId(queryModel);
            return Ok(result);
        }
        [HttpPost("addsmallholding")]
        public async Task<IActionResult> Addsmallholding([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.AddSMH(queryModel);
            return Ok(result);
        }
        [HttpPost("getdatasensorrealtime")]
        public async Task<IActionResult> Getdatarealtime([FromBody]SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetDataSensorRealTime(queryModel);
            return Ok(result);
        }
        [HttpPost("getdatasensorbydate")]
        public async Task<IActionResult> GetdataByDate([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetDataSensorByDate(queryModel);
            return Ok(result);
        }
        [HttpPost("addusertosh")]
        public async Task<IActionResult> AddUserToSH([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.AddUserFromSH(queryModel);
            return Ok(result);
        }
        [HttpPost("getalluser")]
        public async Task<IActionResult> GetallUser([FromBody] SmallHoldingQueryModel queryModel)
        {
            var result = await service.GetAllUser(queryModel);
            return Ok(result);
        }
    }
}
