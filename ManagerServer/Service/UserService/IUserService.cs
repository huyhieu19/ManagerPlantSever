using ManagerServer.Database.Entity;
using ManagerServer.Model.User;

namespace ManagerServer.Service.UserService
{
    public interface IUserService
    {
        public Task<List<AppUser>> GetAll();
        public Task<AppUser> GetById(UserQueryModel query);
        public Task<bool> ChangePassWord(UserQueryModel query);

    }
}
