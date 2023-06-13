using ManagerServer.Service.RoleService;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route ("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        //[HttpGet, Route ("get-all"), Authorize (Roles = "Admin, User, Owner")]
        [HttpGet, Route ("get-all")]

        public async Task<List<string>> GetAll()
        {
            return await roleService.GetAllRole ();
        }

        [HttpPost, Route ("add-role")]
        //[Authorize(Roles = "Admin")]

        public async Task<bool> AddRole(string name)
        {
            return await roleService.AddRoles (name);
        }
        //[HttpPost, Route ("add-user-role"), Authorize (Roles = "Admin, Owner")]
        [HttpPost, Route ("add-user-role")]

        public async Task<bool> AddUserRole(string email, string name)
        {
            return await roleService.AddUserRoles (email, name);
        }
        [HttpDelete, Route ("delete")]

        public async Task<bool> Delete(string name)
        {
            return await roleService.RemoveRoles (name);
        }
    }
}
