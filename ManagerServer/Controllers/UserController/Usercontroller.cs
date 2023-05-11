//using ManagerServer.Model.SMH;
//using ManagerServer.Model.User;
//using ManagerServer.Service.FarmerService;
//using ManagerServer.Service.SmallHoldingServices;
//using Microsoft.AspNetCore.Mvc;

//namespace ManagerServer.Controllers.UserController
//{
//    [ApiController, Route("api/[controller]")]
//    public class Usercontroller: ControllerBase
//    {
//        private readonly IFarmerService farmerService;
//        private readonly ISmallHoldingService smallHoldingService;

//        public Usercontroller(IFarmerService farmerService, ISmallHoldingService smallHoldingService)
//        {
//            this.farmerService = farmerService;
//            this.smallHoldingService = smallHoldingService;
//        }

//        [HttpPost("getdatarealtime")]
//        public async Task<IActionResult> GetDataRealTime([FromBody] SmallHoldingQueryModel queryModel)
//        {
            
//            var result = smallHoldingService.GetDataSensorRealTime(queryModel);
//        }
//    }
//}
