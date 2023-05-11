using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class SmallHoldingEntity
    {
        [Key]
        public int Id { get; set; }
        public int? FarmId { get; set; }
        public FarmEntity? Farm { get; set; }
        public string? NameSmallHolding { get; set; }
        public string? Decription { get; set; }
    }
}
