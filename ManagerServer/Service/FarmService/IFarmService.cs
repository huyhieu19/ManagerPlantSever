using Common.Model.Farm;
using ManagerServer.Database.Entity;

namespace ManagerServer.Service.FarmService
{
    public interface IFarmService
    {
        Task<List<FarmEntity>> GetAll();
        Task<bool> AddFarm(FarmQueryModel query);
        Task<bool> UpdateFarm(FarmQueryModel query);
        Task<FarmEntity> GetById(FarmQueryModel queryModel);
    }
}
