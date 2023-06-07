using Common.Model.Farm;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Farm;
using ManagerServer.Model.ResponeModel;
using ManagerServer.Service.FarmService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route ("api/[controller]")]

    public class FarmController : ControllerBase
    {
        private readonly IFarmService service;

        public FarmController(IFarmService service)
        {
            this.service = service;
        }
        [HttpPost, Route ("add")]
        public async Task<ResponseModel<bool>> AddFarm([FromBody] FarmQueryModel queryModel)
        {
            return await service.AddFarm (queryModel);
        }
        [HttpGet, Route ("get-all")]

        public async Task<List<FarmEntity>> GetAll()
        {
            return await service.GetAll ();
        }
        [HttpPut, Route ("update")]

        public async Task<bool> Update([FromBody] FarmUpdateModel queryModel)
        {
            return await service.UpdateFarm (queryModel);
        }
        [HttpGet, Route ("/{id}")]

        public async Task<FarmEntity> GetById(int Id)
        {
            return await service.GetById (Id);
        }
        [HttpPost, Route("getbytoken")]

        public async Task<List<FarmEntity>> GetbyToken([FromBody] TokenRequestBase requestBase)
        {
            return await service.GetByOwnerId (requestBase);
        }
    }
}
