using Common.Model.Farm;
using ManagerServer.Database.Entity;
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
            return await service.AddFarm (queryModel, getTokenFromHeader (HttpContext));
        }
        [HttpGet, Route ("get-all")]

        public async Task<List<FarmEntity>> GetAll()
        {
            return await service.GetAll ();
        }
        [HttpPut, Route ("update")]

        public async Task<bool> Update([FromBody] FarmUpdateModel queryModel)
        {
            return await service.UpdateFarm (queryModel, getTokenFromHeader (HttpContext));
        }
        [HttpGet, Route ("/{id}")]

        public async Task<FarmEntity> GetById(int Id)
        {
            return await service.GetById (Id);
        }
        [HttpGet, Route ("getbytoken")]
        /// get farm from token
        public async Task<ResponseModel<List<FarmEntity>>> GetbyToken()
        {
            return await service.GetByOwnerId (getTokenFromHeader (HttpContext));
        }
        [HttpGet, Route ("takeheadertoken")]

        public async Task<IActionResult> TakeHeader()
        {

            return Ok (getTokenFromHeader (HttpContext));
        }

        public static string getTokenFromHeader(HttpContext contex)
        {
            contex.Request.Headers.TryGetValue ("Authorization", out var token);
            var result = token.ToString ();
            result = result.Substring (6).Trim ();
            return result;
        }


    }
}
