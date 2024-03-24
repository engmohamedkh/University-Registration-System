using System.ComponentModel.DataAnnotations;

namespace University.Core.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Emil { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}