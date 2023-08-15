using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Operations;

namespace EduHome.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password) ,Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }  
        
    }
}
