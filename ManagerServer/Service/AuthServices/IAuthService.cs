using ManagerServer.Model;

namespace ManagerServer.Service.VisitorServices
{
    public interface IAuthService
    {
        Task<(int, string)> SignUpAsync(SignUpRequestModel model);
        Task<string> SignInAsync(SignInRequestModel model);
    }
}
