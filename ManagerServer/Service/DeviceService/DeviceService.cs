using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.Device;
using ManagerServer.Model.ResponeModel;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly ManagerDbContext dbContext;

        public DeviceService(ManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<DeviceEntity>> GetAllDevice()
        {
            var result = await dbContext.DeviceEntities.ToListAsync ();
            return result;
        }

        public async Task<DeviceEntity> GetById(DeviceRequestModel requestModel)
        {
            return await dbContext.DeviceEntities.FirstOrDefaultAsync (p => p.Id == requestModel.DeviceId);
        }

        public async Task<ResponseModel<List<DeviceEntity>>> GetDeviceAtive(DeviceRequestModel requestModel)
        {
            try
            {
                return new ResponseModel<List<DeviceEntity>> ()
                {
                    code = 1,
                    message = "Get Sucsess",
                    data = await (from data in dbContext.DeviceEntities
                                  where data.Status == true
                                  select data).ToListAsync ()

                };
            }
            catch ( Exception ex )
            {
                return new ResponseModel<List<DeviceEntity>> ()
                {
                    code = 0,
                    message = ex.Message,
                    data = null

                };
            }

        }

        public async Task<ResponseModel<List<DeviceEntity>>> GetDeviceByZoneId(DeviceRequestModel requestModel)
        {
            try
            {
                IQueryable<DeviceEntity> queryDevice = dbContext.DeviceEntities.Where (q => q.ZoneId == requestModel.ZoneId).AsNoTracking ().AsQueryable ();
                if ( !string.IsNullOrEmpty (requestModel.searchTerm) )
                {
                    queryDevice = queryDevice.Where (q => q.Name.ToLower ().Contains (requestModel.searchTerm.ToLower ()));
                }
                if ( requestModel.filterType != Common.Enum.FilterType.None )
                {
                    switch ( requestModel.filterType )
                    {
                        case Common.Enum.FilterType.SortByA_Z:
                            queryDevice = queryDevice.OrderBy (q => q.Name);
                            break;
                        case Common.Enum.FilterType.SortByA_ZReverse:
                            queryDevice = queryDevice.OrderByDescending (q => q.Name);
                            break;
                        case Common.Enum.FilterType.SortByDate:
                            queryDevice = queryDevice.OrderBy (q => q.DateCreate);
                            break;
                        case Common.Enum.FilterType.SortByDateReverse:
                            queryDevice = queryDevice.OrderByDescending (q => q.DateCreate);
                            break;
                    }
                }
                return new ResponseModel<List<DeviceEntity>> ()
                {
                    code = 1,
                    message = "Get Sucsess",
                    data = await queryDevice.ToListAsync ()

                };
            }
            catch ( Exception ex )
            {
                return new ResponseModel<List<DeviceEntity>> ()
                {
                    code = 0,
                    message = ex.Message,
                    data = null

                };
            }
        }

        public async Task<ResponseModel<bool>> SetDeviceToZone(DeviceRequestModel requestModel)
        {
            try
            {
                var device = await dbContext.DeviceEntities.FirstOrDefaultAsync (p => p.Id == requestModel.DeviceId);
                device.ZoneId = requestModel.ZoneId;
                await dbContext.SaveChangesAsync ();
                return new ResponseModel<bool> ()
                {
                    code = 1,
                    message = "Set Sucsess",
                    data = true,

                };
            }
            catch ( Exception ex )
            {
                return new ResponseModel<bool> ()
                {
                    code = 0,
                    message = ex.Message,
                    data = false

                };
            }
        }
    }
}
