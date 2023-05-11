using ManagerServer.Common;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly ManagerDbContext context;

        public TestController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager,ManagerDbContext context )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
        #region Role
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleRequestModel model)
        {
            if ( await roleManager.RoleExistsAsync(model.Role))
            {
                return BadRequest("Role exist!");
            }
            var role = new IdentityRole();
            role.Name = model.Role;
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest("Add fail");
            }
            return Ok("Succes");
        }
        [HttpGet("GetRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var roles = roleManager.Roles.ToList();
            return Ok(roles);
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await userManager.Users.ToListAsync();
            return Ok(result);
        }
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            try
            {
                var userTemp = await userManager.FindByEmailAsync(email);
                var result = await userManager.AddToRoleAsync(userTemp, roleName);
                if (result.Succeeded) return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("GetUserRole")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            try
            {
                var userTemp =await userManager.FindByEmailAsync(email);
                var result =await userManager.GetRolesAsync(userTemp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email,string roleName)
        {
            try
            {
                var userTemp = await userManager.FindByEmailAsync(email);
                var result = await userManager.RemoveFromRoleAsync(userTemp, roleName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("GetAllClaims")]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            try
            {
                var userTemp = await userManager.FindByEmailAsync(email);
                var result = await userManager.GetClaimsAsync(userTemp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("AddClaimsToUser")]
        public async Task<IActionResult> AddClaimsToUser(string email,string claimName, string claimValue)
        {
            try
            {
                var userTemp = await userManager.FindByEmailAsync(email);
                var result = await userManager.AddClaimAsync(userTemp,new Claim(claimName, claimValue));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        [HttpPost("generateDataProcesscing")]
        public async Task<bool> GenerateDataProc(int amount)
        {
            var list = GenerateData<DataProcessingEntity>.CreateListData(amount);
            foreach(var item in list)
            {
               var adddata = new DataProcessingEntity()
                {
                    FarmId = item.FarmId,
                    Topic = item.Topic,
                    Payload = item.Payload,
                    Mode = item.Mode,
                    RetrieveAt = item.RetrieveAt,
                    SmallHoldingId = item.SmallHoldingId,
                };
                await context.DataProcessingEntites.AddAsync(adddata);
                await context.SaveChangesAsync();
            }
           

            return true;
        }
    }
}
