using Microsoft.AspNetCore.Components.QuickGrid;
using Proyecto_Web.Models;

namespace Proyecto_Web.ViewModels.Categories
{
    public class CategoryListViewModel : ViewModelBase
	{
		public string Search {  get; set; }

		public List<Category> Categories { get; set; }

		public Pagination Pagination { get; set; }	
	}

	
}
