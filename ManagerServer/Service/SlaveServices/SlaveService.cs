using Common.Model.SlaveModel;
using ManagerServer.Database;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Service.SlaveServices
{
    public class SlaveService : ISlaveService
    {
        private readonly ManagerDbContext context;

        public SlaveService(ManagerDbContext context)
        {
            this.context = context;
        }
        public async Task<List<SlaveDisplayDataModel>> GetDataRealTime(SlaveQueryDataModel queryModel)
        {

            var query = from zone in context.SmallHoldingEntities
                        join data in context.DataProcessingEntites on zone.Id equals data.SmallHoldingId
                        where data.SmallHoldingId == zone.Id
                        select new { data.Topic, data.Payload, data.RetrieveAt };
            var list = await query.Select(p => new SlaveDisplayDataModel()
            {
                Topic = p.Topic,
                Payload = p.Payload,
                ValueDate = p.RetrieveAt,
            }).ToListAsync();
            return list;
        }
    }
}
