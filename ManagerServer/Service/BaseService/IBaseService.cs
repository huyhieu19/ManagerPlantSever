using ManagerServer.Model;

namespace ManagerServer.Service.BaseService
{
    public interface IBaseService
    {
        Task<T> GetAsync<T>(string id, BaseQueryModel queryModel);
    }
}
