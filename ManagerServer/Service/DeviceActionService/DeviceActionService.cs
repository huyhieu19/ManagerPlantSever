using ManagerServer.Common.Mapper;
using ManagerServer.Database;
using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.DeviceActionService
{
    public sealed class DeviceActionService : IDeviceActionService
    {
        private readonly ManagerDbContext dbContext;

        public Task<DeviceActionDisplayModel> DeleteDeviceAction(int zoneId)
        {
            throw new NotImplementedException ();
        }


        public async Task<ResponseModel<List<DeviceActionDisplayModel>>> GetDeviceActionByZoneId(int zoneId)
        {
            try
            {
                var deviceActions = from data in dbContext.DeviceActionEntities
                                    where zoneId == data.zoneId
                                    select data.DeviceActionMapping ();
                return new ResponseModel<List<DeviceActionDisplayModel>> ()
                {
                    code = 1,
                    message = "Success",
                    data = await deviceActions.ToListAsync (),
                };
            }
            catch ( Exception ex )
            {
                return new ResponseModel<List<DeviceActionDisplayModel>> ()
                {
                    code = 0,
                    message = "Fall" + ex.Message,
                };
            }
        }

        public Task<DeviceActionDisplayModel> UpdateDeviceAction(int zoneId)
        {
            throw new NotImplementedException ();
        }
    }
}
