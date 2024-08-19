using Proyecto_Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Users
{
	public class UpdateUsersViewModel : ViewModelBase
	{
		public int UserId { get; set; }
		[Required]
		[Display(Name = "First Name")]
		public string? FirtName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string? LastName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Emial Address")]
		public string? Email { get; set; }

		[Required]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Required]
		[Display(Name = "Role:")]
		public string? Role { get; set; }
	}
}
