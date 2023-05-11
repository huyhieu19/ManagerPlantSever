using Common.Model.SlaveModel;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Service.SlaveServices
{
    public interface ISlaveService
    {
        public Task<List<SlaveDisplayDataModel>> GetDataRealTime(SlaveQueryDataModel queryModel);
    }
}
