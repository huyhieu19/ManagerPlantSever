using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class DataEntity
    {
        [Key]
        public int Id { get; set; } 
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public DateTime? RetrieveAt { get; set; }
        public string? Type { get; set; }
        public int? DeviceId { get; set; }
        public DeviceEntity? Device { get; set; }
    }
}
