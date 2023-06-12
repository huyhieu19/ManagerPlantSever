using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class ZoneEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey ("FarmEntity")]
        public int? FarmId { get; set; }
        public string? Name { get; set; }
        public string? Decription { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public string? Image { get; set; }
        public List<DeviceEntity>? DeviceEntity { get; set; }
        public List<DeviceActionEntity>? DeviceActionEntity { get; set; }
    }
}
