
using Microsoft.AspNetCore.Identity;

namespace ManagerServer.Database.Entity
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avata { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public bool? is_ative { get; set; } = false;
        public string? Adress { get; set; }

        public List<FarmEntity>? Farms { get; set; }

    }
}
