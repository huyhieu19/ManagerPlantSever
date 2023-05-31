using ManagerServer.Database.Entity;
using ManagerServer.Model.Zone;
using ManagerServer.Service.ZoneService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService service;

        public ZoneController(IZoneService service)
        {
            this.service = service;
        }
        [HttpPost,Route("create")]
        public async Task<bool> CreateZone([FromBody]ZoneQueryModel queryModel)
        {
            return await service.CreateZone(queryModel);
        }
        [HttpGet, Route("get-all")]
        public async Task<List<ZoneEntity>> GetAllZones()
        {
            return await service.GetAllZones();
        }
        [HttpPost, Route("get-byid")]
        public async Task<ZoneEntity> GetZoneById(ZoneQueryModel queryModel)
        {
           return await service.GetZoneById(queryModel);
        }
        [HttpPost, Route("update")]
        public async Task<bool> UpdateZone(ZoneQueryModel queryModel)
        {
           return await service.UpdateZone(queryModel);
        }
    }
}
