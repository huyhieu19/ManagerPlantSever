using ManagerServer.Database.Entity;
using ManagerServer.Model.Zone;

namespace ManagerServer.Service.ZoneService
{
    public interface IZoneService
    {
        public Task<List<ZoneEntity>> GetAllZones();
        public Task<ZoneEntity> GetZoneById(ZoneQueryModel queryModel);
        public Task<bool> CreateZone(ZoneQueryModel queryModel);
        public Task<bool> UpdateZone(ZoneQueryModel queryModel);
    }
}
