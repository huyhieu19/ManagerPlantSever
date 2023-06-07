using System.ComponentModel.DataAnnotations;

namespace Common.Model.Farm
{
    public class FarmQueryModel
    {
        [Required]
        public string? FarmName { get; set; }
        [Required]
        public string? FarmDescription { get; set; }
        [Required]
        public string? Avata { get; set; }
        public string? Adress { get; set; }
    }
}
