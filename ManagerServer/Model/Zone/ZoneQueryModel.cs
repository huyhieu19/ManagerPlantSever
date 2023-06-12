namespace ManagerServer.Model.Zone
{
    public class ZoneQueryModel : BaseQueryModel
    {
        public int? Id { get; set; }
        public string? ZoneName { get; set; }
        public string? Decription { get; set; }
        public int? FarmId { get; set; }
        public string? Image { get; set; }


    }
}
