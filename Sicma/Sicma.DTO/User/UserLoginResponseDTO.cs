using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicma.DTO.User
{
    public class UserLoginResponseDTO
    {
        public UserDataDTO UserData { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
