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
