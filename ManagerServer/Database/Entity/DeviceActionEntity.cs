using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class DeviceActionEntity
    {
        [Key]
        public int id { get; set; }
        public string nameDevice { get; set; } = string.Empty;
        public string descriptionDevice { get; set; } = string.Empty;
        public bool isProblem { get; set; } = false;
        public bool isAction { get; set; } = false;
        public string image { get; set; } = string.Empty;
        public DateTime dateCreate { get; set; }
        [ForeignKey ("ZoneEntity")]
        public int zoneId { get; set; }
        public ZoneEntity? zoneEntity { get; set; }
    }
}
