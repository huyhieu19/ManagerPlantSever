using ManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.BaseService
{
    public class BaseService : IBaseService
    {
        public DbContext dbContext { get; set; }

        public Task<T> GetAsync<T>(string id, BaseQueryModel queryModel)
        {
            throw new NotImplementedException ();
        }
    }
}
