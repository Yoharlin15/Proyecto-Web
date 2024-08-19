using Proyecto_Web.Models;
using Proyecto_Web.ViewModels.Countries;

namespace Proyecto_Web.ViewModels.Countries
{
	public class CountryListViewModel
	{
		public string Search { get; set; }
		public List<Country> Countries { get; set; }
		public Pagination Pagination { get; set; }
	}
}
