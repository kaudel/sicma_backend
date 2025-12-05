using System.ComponentModel.DataAnnotations;

namespace Sicma.DTO.User
{
    public class UserLoginRequestDTO
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
