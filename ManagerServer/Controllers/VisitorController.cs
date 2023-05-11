using ManagerServer.Database;
using ManagerServer.Model;
using ManagerServer.Service.VisitorServices;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService visitorService;
        private readonly ManagerDbContext context;

        public VisitorController(IVisitorService visitorService,ManagerDbContext context)
        {
            this.visitorService = visitorService;
            this.context = context;
        }
        [HttpPost("signUp")]
        public async Task<IActionResult> SingnUp([FromBody] SignUpRequestModel model)
        {
            var (result,id) = await visitorService.SignUpAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(new
            {
                token= result,
                value = id
            });
        }
        [HttpPost("signIn")]
        public async Task<IActionResult> SingnIn([FromBody] SignInRequestModel model)
        {
          try
            {
                var (result, id) = await visitorService.SignInAsync(model);
                if (string.IsNullOrEmpty(result))
                {
                    return Ok(new
                    {
                        code = -1,
                    });
                }
                return Ok(new
                {
                    code = 0,
                    token = result,
                    value = id,
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
