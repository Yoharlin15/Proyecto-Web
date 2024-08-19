using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Categories
{
    public class UpdateCategoryViewModel : ViewModelBase
    {
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        [Required]
        public string? Description { get; set; }
	}
}
