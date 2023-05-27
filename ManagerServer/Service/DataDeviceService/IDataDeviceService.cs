using ManagerServer.Model.DataDeviceModel;
using ManagerServer.Model.DataDisplay;
using ManagerServer.Model.SMH;

namespace ManagerServer.Service.DataDeviceService
{
    public interface IDataDeviceService
    {
        Task<int> GetDataDeviceId(DataDeviceQueryModel queryModel);
    }
}
