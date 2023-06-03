using ManagerServer.Common.Enum;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Admin;
using ManagerServer.Model.Owner;
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
            var user = await userManager.FindByIdAsync (query.Id);
            var result = await userManager.ChangePasswordAsync (user, query.Password, query.NewPassWord);
            return result.Succeeded;
        }

        public async Task<List<AppUser>> GetAll()
        {
            var result = await userManager.Users.ToListAsync ();
            return result;
        }

        public async Task<List<AdminDisplayModel>> GetAllAdmin()
        {
            var admins = await userManager.GetUsersInRoleAsync (UserRoles.Admin.ToString ());
            var result = admins.Select (q => new AdminDisplayModel
            {
                AdminId = q.Id,
                EmailAddress = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList ();

            return result;
        }

        public async Task<List<OwnerDisplayModel>> GetAllOwner()
        {
            var owners = await userManager.GetUsersInRoleAsync (UserRoles.Owner.ToString ());
            var result = owners.Select (q => new OwnerDisplayModel
            {
                OwnerId = q.Id,
                EmailAddress = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList ();

            return result;
        }

        public async Task<List<UserDisplayModel>> GetAllUser()
        {
            var users = await userManager.GetUsersInRoleAsync (UserRoles.User.ToString ());
            var result = users.Select (q => new UserDisplayModel
            {
                UserId = q.Id,
                EmailAddress = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList ();

            return result;
        }

        public async Task<AppUser> GetById(string Id)
        {
            return await userManager.FindByIdAsync (Id);
        }
    }
}
