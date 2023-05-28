using ManagerServer.Database.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<bool> AddRoles(string name)
        {
            var result =  await roleManager.CreateAsync(new IdentityRole(name));
            return result.Succeeded;
        }

        public async Task<bool> AddUserRoles(string email, string nameRole)
        {
            var userTemp = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRoleAsync(userTemp, nameRole);
            return result.Succeeded;
        }

        public async Task<List<string>> GetAllRole()
        {
            var result = new List<string>();
            var roles = await roleManager.Roles.ToListAsync();

            foreach(var role in roles)
            {
                result.Add(role.Name);
            }
            return result; 
        }

        public async Task<bool> RemoveRoles(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            var result =  await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
    }
}
