using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Account
{
    public class AccountLoginViewModel : ViewModelBase
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]    
        public string? Password { get; set; }
    }
}
