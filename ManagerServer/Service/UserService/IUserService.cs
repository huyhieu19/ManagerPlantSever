namespace ManagerServer.Service.UserService
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(string Id);
        Task<bool> ChangePassWord(UserQueryModel query);
        Task<List<AdminDisplayModel>> GetAllAdmin();
        Task<List<OwnerDisplayModel>> GetAllOwner();
        Task<List<UserDisplayModel>> GetAllUser();

    }
}
