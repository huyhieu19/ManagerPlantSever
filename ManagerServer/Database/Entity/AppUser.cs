
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerServer.Database.Entity
{
    public class AppUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public List<FarmEntity>? Farms { get; set; }
        public string? Adress { get; set; }

    }
}
