using ManagerServer.Database.Entity;
using ManagerServer.Model.Zone;

namespace ManagerServer.Service.ZoneService
{
    public interface IZoneService
    {
        Task<List<ZoneEntity>> GetAllZones();
        Task<ZoneEntity> GetZoneById(ZoneQueryModel queryModel);
        Task<bool> CreateZone(ZoneQueryModel queryModel);
        Task<bool> UpdateZone(ZoneQueryModel queryModel);
    }
}
