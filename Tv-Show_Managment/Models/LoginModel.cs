using System.ComponentModel.DataAnnotations;

namespace Tv_Show_Managment.Models
{
    public class LoginModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }

        //user: jameel , pass: 1234
    }
}
