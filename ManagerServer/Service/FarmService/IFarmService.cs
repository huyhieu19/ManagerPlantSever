using Common.Model.Farm;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Farm;
using ManagerServer.Model.ResponeModel;

namespace ManagerServer.Service.FarmService
{
    public interface IFarmService
    {
        Task<List<FarmEntity>> GetAll();
        Task<ResponseModel<bool>> AddFarm(FarmQueryModel query);
        Task<bool> UpdateFarm(FarmUpdateModel query);
        Task<FarmEntity> GetById(int Id);
    }
}
