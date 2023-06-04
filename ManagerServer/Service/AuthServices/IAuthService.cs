using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Authr;

namespace ManagerServer.Service.VisitorServices
{
    public interface IAuthService
    {
        Task<(int, string)> SignUpAsync(SignUpRequestModel model);
        Task<string> SignInAsync(SignInRequestModel model);
        Task<AppUser> Getinfo(AutherRequest request);
    }
}
