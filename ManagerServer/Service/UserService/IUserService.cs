using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Admin;
using ManagerServer.Model.Owner;
using ManagerServer.Model.ResponeModel;
using ManagerServer.Model.User;

namespace ManagerServer.Service.UserService
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(string Id);
        Task<bool> ChangePassWord(UserQueryModel query);
        Task<List<AdminDisplayModel>> GetAllAdmin();
        Task<ResponseModel<List<OwnerDisplayModel>>> GetAllOwner(BaseQueryModel baseQueryModel);
        Task<List<UserDisplayModel>> GetAllUser();

    }
}
