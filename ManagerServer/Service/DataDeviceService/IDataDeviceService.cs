using ManagerServer.Model.DataDisplay;
using ManagerServer.Model.SMH;

namespace ManagerServer.Service.DataDeviceService
{
    public interface IDataDeviceService
    {
        Task<DataSensorDisplayModel> GetDataSensorRealTime(SmallHoldingQueryModel queryModel);
    }
}
