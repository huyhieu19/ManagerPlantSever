using ManagerServer.Database.Entity;
using ManagerServer.Model.User;
using ManagerServer.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route ("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }
        [HttpPost, Route ("change-password")]
        [Authorize (Roles = "User, Admin")]
        public async Task<bool> ChangePassWord(UserQueryModel query)
        {
            return await service.ChangePassWord (query);
        }
        [HttpGet]
        [Route ("get-all")]
        [Authorize (Roles = "Admin, User")]

        public async Task<List<AppUser>> GetAll()
        {
            return await service.GetAll ();
        }
        [HttpPost, Route ("get-byid")]

        public async Task<AppUser> GetById(UserQueryModel query)
        {
            return await service.GetById (query);
        }
    }
}
