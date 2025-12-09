using System.ComponentModel.DataAnnotations;

namespace Sicma.DTO.Request.User
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage= "Username is mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is mandatory")]
        public string Password { get; set; }
    }
}
