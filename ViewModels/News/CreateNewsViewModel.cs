using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.News
{
    public class CreateNewsViewModel : ViewModelBase
    {
        [Required]
        public string? Name { get; set; }
        public string? PicturePath { get; set; }
        public IFormFile PictureFile { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateOnly PubishedDate { get; set; }
        [Required]
        public string Author { get; set; }
        public int UserId { get; set; }
    }
}
