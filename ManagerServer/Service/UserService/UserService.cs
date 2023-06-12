using ManagerServer.Common.Enum;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Admin;
using ManagerServer.Model.Owner;
using ManagerServer.Model.ResponeModel;
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

        public async Task<ResponseModel<List<OwnerDisplayModel>>> GetAllOwner(BaseQueryModel baseQueryModel)
        {
            try
            {
                var owners = await userManager.GetUsersInRoleAsync (UserRoles.Owner.ToString ());
                string searchTerm = baseQueryModel.searchTerm.ToUpper ().Trim ();
                if ( !string.IsNullOrEmpty (baseQueryModel.searchTerm) )
                {
                    owners = owners.Where (q => q.UserName.ToUpper ().Contains (searchTerm) || q.Email.Contains (searchTerm)).ToList ();
                }
                if ( baseQueryModel.filterType != FilterType.None )
                {
                    switch ( baseQueryModel.filterType )
                    {
                        case FilterType.SortByA_Z:
                            owners = owners.OrderBy (q => q.UserName).ToList ();
                            break;
                        case FilterType.SortByA_ZReverse:
                            owners = owners.OrderByDescending (q => q.UserName).ToList ();
                            break;
                        case FilterType.SortByDate:
                            owners = owners.OrderBy (q => q.CreateAt).ToList ();
                            break;
                        case FilterType.SortByDateReverse:
                            owners = owners.OrderByDescending (q => q.CreateAt).ToList ();
                            break;
                    }
                }
                var result = owners.Select (q => new OwnerDisplayModel
                {
                    OwnerId = q.Id,
                    EmailAddress = q.Email,
                    FirstName = q.FirstName,
                    LastName = q.LastName
                }).ToList ();
                return new ResponseModel<List<OwnerDisplayModel>> ()
                {
                    code = 0,
                    message = "Success",
                    data = result,
                };

            }
            catch ( Exception ex )
            {
                return new ResponseModel<List<OwnerDisplayModel>> ()
                {
                    code = 0,
                    message = ex.Message
                };
            }
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
