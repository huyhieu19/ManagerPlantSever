namespace ManagerServer.Model.Device
{
    public class DeviceActionUpdateModel
    {
        public int id { get; set; }
        public string nameDevice { get; set; } = string.Empty;
        public string descriptionDevice { get; set; } = string.Empty;
        public bool isProblem { get; set; } = false;
        public bool isAction { get; set; } = false;
        public string image { get; set; } = string.Empty;
        public int zoneId { get; set; }
    }
}
