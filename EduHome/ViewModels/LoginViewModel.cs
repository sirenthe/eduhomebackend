using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UsernameorEmail { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
