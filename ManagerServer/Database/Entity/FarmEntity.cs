using System.ComponentModel.DataAnnotations;

namespace ManagerServer.Database.Entity
{
    public class FarmEntity
    {
        [Key]
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public AppUser? Owner { get; set; }
        public string? Name { get; set; }
        public string? Decription { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public string? Avata { get; set; }
        public string? Adress { get; set; }
        public List<ZoneEntity>? Zones { get; set; }

    }
}
