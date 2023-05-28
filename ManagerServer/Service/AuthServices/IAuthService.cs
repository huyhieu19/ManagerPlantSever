using ManagerServer.Model;
using Microsoft.AspNetCore.Identity;

namespace ManagerServer.Service.VisitorServices
{
    public interface IAuthService
    {
        public Task<(int,string)> SignUpAsync(SignUpRequestModel model);
        public Task<string> SignInAsync(SignInRequestModel model);
    }
}
