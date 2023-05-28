using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class DeviceEntity
    {
        [Key]
        public int Id{ get; set; }
        public string? Name { get; set; }
        public string? Decription { get; set; }
        public DateTime? DateCreate { get; set; } = DateTime.Now;
        public bool? Status { get; set; } = true;
        [ForeignKey("ZoneEntity")]
        public int? ZoneId { get; set; }
        //public ZoneEntity? Zone { get; set; }
        public List<DataEntity>? Datas { get; set; }
        [InverseProperty("DeviceEntity")]
        public virtual ZoneEntity? ZoneEntity { get; set; }
    }
}
