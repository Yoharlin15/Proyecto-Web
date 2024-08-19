using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.News
{
    public class UpdateNewsViewModel : ViewModelBase
    {
        public int NewsId { get; set; } 
        [Required]
        public string? Title { get; set; }
        public string? PicturePath { get; set; }
        public IFormFile PictureFile { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateOnly? PubishedDate { get; set; }
        [Required]
        public string? Author { get; set; }
    }
}
