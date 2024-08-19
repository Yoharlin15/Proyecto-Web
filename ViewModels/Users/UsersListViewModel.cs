using Proyecto_Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Users
{
    public class UsersListViewModel
    {
		public string Search { get; set; }

		public List<User> User { get; set; }

		public Pagination Pagination { get; set; }
	}
}
