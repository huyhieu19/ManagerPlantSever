namespace ManagerServer.Service.RoleService
{
    public interface IRoleService
    {
        Task<List<string>> GetAllRole();
        Task<bool> AddRoles(string name);
        Task<bool> RemoveRoles(string name);
        Task<bool> AddUserRoles(string userId, string nameRole);
    }
}
