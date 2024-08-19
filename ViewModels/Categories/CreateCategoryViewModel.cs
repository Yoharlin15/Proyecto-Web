using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Categories
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        [Required]
        
        public string? CategoryName { get; set; }

        [Required]
        public string? Description { get; set; }
		
	}
}
