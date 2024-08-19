using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Categories
{
    public class DeleteCategoryViewModel : ViewModelBase
	{
		public int CategoryId { get; set; }

		[Required]
		[Display(Name = "Category Name")]
		public string? CategoryName { get; set; }

		[Required]
		[Display(Name ="Description")]
		public string? Description { get; set; }
	}
}
