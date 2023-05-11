using System.ComponentModel.DataAnnotations;

namespace ManagerServer.Model
{
    public class SignInRequestModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]        
        public string Password { get; set; } = null!;
    }
}
