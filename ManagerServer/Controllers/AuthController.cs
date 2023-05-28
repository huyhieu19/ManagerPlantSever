using ManagerServer.Database;
using ManagerServer.Model;
using ManagerServer.Service.VisitorServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService visitorService;
        private readonly ManagerDbContext context;

        public AuthController(IAuthService visitorService, ManagerDbContext context)
        {
            this.visitorService = visitorService;
            this.context = context;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SingnUp([FromBody] SignUpRequestModel model)
        {
            var (code,token) = await visitorService.SignUpAsync(model);
            
            return new ObjectResult(new
            {
                code = code,
                token = token
                
            });
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SingnIn([FromBody] SignInRequestModel model)
        {
            try
            {
                var result = await visitorService.SignInAsync(model);
                if (string.IsNullOrEmpty(result))
                {

                    return new ObjectResult(new {
                    code = -1,
                    token = ""
                    });
                }

                return new ObjectResult( new
                {
                    code = 0,
                    token = result,
                });
                
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    code = -1,
                });
            }
        }
    }
}
