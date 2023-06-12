namespace ManagerServer.Model.Device
{
    public class DeviceRequestModel : BaseQueryModel
    {
        public string? DeviceId { get; set; }
        public int? ZoneId { get; set; }
    }
}
