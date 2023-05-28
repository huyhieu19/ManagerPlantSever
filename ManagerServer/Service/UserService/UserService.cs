using ManagerServer.Database.Entity;
using ManagerServer.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<bool> ChangePassWord(UserQueryModel query)
        {
            var user = await userManager.FindByIdAsync(query.Id);
            var result = await userManager.ChangePasswordAsync(user, query.Password, query.NewPassWord);
            return result.Succeeded;
        }

        public async Task<List<AppUser>> GetAll()
        {
            var result = await userManager.Users.ToListAsync();
            return result;
        }

        public async Task<AppUser> GetById(UserQueryModel query)
        {
            return  await userManager.FindByIdAsync(query.Id);
        }
    }
}
