using ManagerServer.Model;
using Microsoft.AspNetCore.Identity;

namespace ManagerServer.Service.VisitorServices
{
    public interface IVisitorService
    {
        public Task<(string, string)> SignUpAsync(SignUpRequestModel model);
        public Task<(string, string)> SignInAsync(SignInRequestModel model);
    }
}
