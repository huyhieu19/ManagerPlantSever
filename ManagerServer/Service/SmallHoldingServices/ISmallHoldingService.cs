using Common.Model;
using ManagerServer.Model;
using ManagerServer.Model.DataDisplay;
using ManagerServer.Model.Owner;
using ManagerServer.Model.SMH;

namespace ManagerServer.Service.SmallHoldingServices
{ 
    public interface ISmallHoldingService 
    {
        Task<SmallHoldingDiplayModel> GetSMHById(SmallHoldingQueryModel queryModel);
        Task<PagingReponseModel<SmallHoldingDiplayModel>> GetSMHByFarmId(SmallHoldingQueryModel queryModel);
        Task<bool> AddSMH(SmallHoldingQueryModel queryModel);
        Task<bool> DeleteSHM(SmallHoldingQueryModel queryModel);
        Task<bool> EditSHM(SmallHoldingQueryModel queryModel);
        Task<PagingReponseModel<SmallHoldingDiplayModel>> GetAllSHM(SmallHoldingQueryModel queryModel);
        Task<PagingReponseModel<OwnerDisplayModel>> GetAllUser(SmallHoldingQueryModel queryModel);
        Task<DataSensorDisplayModel> GetDataSensorRealTime(SmallHoldingQueryModel queryModel);
        Task<List<DataSensorDisplayModel>> GetDataSensorByDate(SmallHoldingQueryModel queryModel);
        Task<bool> AddUserFromSH(SmallHoldingQueryModel queryModel);


    }
}
