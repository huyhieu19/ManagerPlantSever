using ManagerServer.Database.Entity;
using ManagerServer.Model.User;

namespace ManagerServer.Service.UserService
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(UserQueryModel query);
        Task<bool> ChangePassWord(UserQueryModel query);

    }
}
