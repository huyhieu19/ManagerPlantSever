using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class FarmEntity
    {
        [Key]
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public AppUser? Owner { get; set; }
        public List<SmallHoldingEntity>? SmallHoldings { get; set; }
        public string? Name { get; set; }
        public string? Decription { get; set; }
    }
}
