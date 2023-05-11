using Common.Model.Farm;
using ManagerServer.Model;
using ManagerServer.Service.FarmerService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]

    public class FarmController : ControllerBase
    {
        private readonly IFarmerService farmerService;

        public FarmController(IFarmerService farmerService)
        {
            this.farmerService = farmerService;
        }
        [HttpPost("getallfarm")]
        public async Task<IActionResult> GetAllFarm(FarmQueryModel queryModel)
        {
           try
            {
                var result = await farmerService.GetAllFarm(queryModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            
        }
        [HttpPost("getafarmbyid")]
        public async Task<IActionResult> GetFarmbyId(FarmQueryModel queryModel)
        {
            try
            {
                var result = await farmerService.GetFarmById(queryModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Exception");
            }

        }
        [HttpPost("getafarmbyownerid")]
        public async Task<IActionResult> GetFarmbyOwnerId(FarmQueryModel queryModel)
        {
            try
            {
                var result = await farmerService.GetFarmByOwnerId(queryModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Exception");
            }

        }
        [HttpPost("addafarm")]
        public async Task<IActionResult> AddAFarm(FarmQueryModel queryModel)
        {
            try
            {
                var result = await farmerService.AddFarm(queryModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exception: "+ ex);
            }
        }
    }
}
