using ManagerServer.Database.Entity;
using ManagerServer.Model.Device;

namespace ManagerServer.Common.Mapper
{
    public static class DeviceActionMapper
    {
        public static DeviceActionDisplayModel DeviceActionMapping(this DeviceActionEntity entity)
        {
            return new DeviceActionDisplayModel ()
            {
                Id = entity.id,
                DescriptionDevice = entity.descriptionDevice,
                IsAction = entity.isAction,
                IsProblem = entity.isProblem,
                NameDevice = entity.nameDevice,
                ZoneId = entity.zoneId,
            };
        }
    }
}
