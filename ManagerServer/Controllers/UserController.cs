using ManagerServer.Database.Entity;
using ManagerServer.Model.Admin;
using ManagerServer.Model.Owner;
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
        [HttpGet, Route ("get-byid")]

        public async Task<AppUser> GetById([FromQuery] string Id)
        {
            return await service.GetById (Id);
        }

        [HttpGet, Route ("admins")]
        public async Task<List<AdminDisplayModel>> GetAllAdmin()
        {
            return await service.GetAllAdmin ();
        }
        [HttpGet, Route ("owners")]
        public async Task<List<OwnerDisplayModel>> GetAllOwner()
        {
            return await service.GetAllOwner ();
        }
        [HttpGet, Route ("users")]
        public async Task<List<UserDisplayModel>> GetAllUser()
        {
            return await service.GetAllUser ();
        }
    }
}
