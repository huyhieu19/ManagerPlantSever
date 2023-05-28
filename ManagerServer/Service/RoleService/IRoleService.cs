namespace ManagerServer.Service.RoleService
{
    public interface IRoleService
    {
        public Task<List<string>> GetAllRole();
        public Task<bool> AddRoles(string name);
        public Task<bool> RemoveRoles(string name);
        public Task<bool> AddUserRoles(string userId, string nameRole);
    }
}
