using ManagerServer.Model;
using System.ComponentModel.DataAnnotations;

namespace Common.Model.Farm
{
    public class FarmQueryModel: TokenRequestBase
    {
        [Required]
        public string OwnerID { get; set; } = string.Empty;
        [Required]
        public string? FarmName { get; set; }
        [Required]
        public string? FarmDescription { get; set; }
        [Required]
        public string? Avata { get; set; }
        public string? Adress { get; set; }
    }
}
