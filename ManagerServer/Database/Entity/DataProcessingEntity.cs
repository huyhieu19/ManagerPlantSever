using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class DataProcessingEntity
    {
        [Key]
        public int Id { get; set; } 
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public DateTime? RetrieveAt { get; set; }
        public int? FarmId { get; set; }
        public string? Mode { get; set; }
        public int SmallHoldingId { get; set; }
        public SmallHoldingEntity? SmallHolding { get; set; }
    }
}
