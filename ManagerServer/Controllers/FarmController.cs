using Common.Model.Farm;
using ManagerServer.Database.Entity;
using ManagerServer.Service.FarmService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]

    public class FarmController: ControllerBase
    {
        private readonly IFarmService service;

        public FarmController(IFarmService service)
        {
            this.service = service;
        }
        [HttpPost, Route("add")]
        public async Task<bool> AddFarm([FromBody]FarmQueryModel queryModel)
        {
            return await service.AddFarm(queryModel);
        }
        [HttpGet, Route("get-all")]

        public async Task<List<FarmEntity>> GetAll()
        {
            return await service.GetAll();
        }
        [HttpPut, Route("update")]

        public async Task<bool> Update([FromBody] FarmQueryModel queryModel)
        {
            return await service.UpdateFarm(queryModel);
        }
        [HttpPost, Route("get-byid")]

        public async Task<FarmEntity> GetById([FromBody] FarmQueryModel queryModel)
        {
            return await service.GetById(queryModel);
        }
    }
}
