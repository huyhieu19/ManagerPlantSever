using Common.Model.Farm;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.Owner;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Service.FarmerService
{
    public interface IFarmerService
    {
        Task<bool> AddFarm(FarmQueryModel queryModel);
        Task<bool> EditFarm(FarmQueryModel queryModel);
        Task<PagingReponseModel<FarmDisplayModel>> GetAllFarm(FarmQueryModel queryModel);
        Task<bool> DeleteFarm(FarmQueryModel queryModel);
        Task<FarmDisplayModel> GetFarmById(FarmQueryModel queryModel);
        Task<PagingReponseModel<FarmDisplayModel>> GetFarmByOwnerId(FarmQueryModel queryModel);
        Task<OwnerDisplayModel> GetOwner(FarmQueryModel queryModel);
    }
}
