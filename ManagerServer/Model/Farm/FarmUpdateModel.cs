using System.ComponentModel.DataAnnotations;

namespace ManagerServer.Model.Farm
{
    public class FarmUpdateModel
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string? OwnerID { get; set; }
        [Required]
        public string? FarmName { get; set; }
        [Required]
        public string? FarmDescription { get; set; }
        [Required]
        public string? Avata { get; set; }
        public string? Adress { get; set; }
    }
}
