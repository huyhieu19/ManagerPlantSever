namespace ManagerServer.Model.Device
{
    public class DeviceActionDisplayModel
    {
        public int Id { get; set; }
        public string NameDevice { get; set; } = string.Empty;
        public string DescriptionDevice { get; set; } = string.Empty;
        public int ZoneId { get; set; }
        public bool IsAction { get; set; } = false;
        public bool IsProblem { get; set; } = false;
    }
}
