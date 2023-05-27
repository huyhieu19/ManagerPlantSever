using ManagerServer.Database;
using ManagerServer.Model.DataDeviceModel;
using ManagerServer.Model.DataDisplay;
using ManagerServer.Model.SMH;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.DataDeviceService
{
    public class DataDeviceService : IDataDeviceService
    {
        private readonly ManagerDbContext dbContext;

        public DataDeviceService(ManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> GetDataDeviceId(DataDeviceQueryModel queryModel)
        {
            var query = dbContext.DataProcessingEntites.AsQueryable();
            query = query.Where(p=>p.SmallHoldingId == queryModel.ShId);
            var result = await query.FirstOrDefaultAsync();
            if (result == null)
            {
                return -1;
            }
            return result.Id;
        }
    }
}
