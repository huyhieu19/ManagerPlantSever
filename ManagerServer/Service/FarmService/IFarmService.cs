using Common.Model.Farm;
using ManagerServer.Database.Entity;

namespace ManagerServer.Service.FarmService
{
    public interface IFarmService
    {
        public Task<List<FarmEntity>> GetAll();
        public Task<bool> AddFarm(FarmQueryModel query);
        public Task<bool> UpdateFarm(FarmQueryModel query);
    }
}
