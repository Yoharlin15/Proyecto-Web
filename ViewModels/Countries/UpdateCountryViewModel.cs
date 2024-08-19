using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Countries
{
    public class UpdateCountryViewModel : ViewModelBase
    {
        public int CountryId {get; set; }
        [Required]
        public string? CountryName { get; set; }
        [Required]
        public string? Capital { get; set; }
        [Required]
        public string? Population { get; set; }
        [Required]
        public string? Currency { get; set; }
        [Required]
        public string? Continent { get; set; }
        [Required]
        public string? Language { get; set; }
        public string? PicturePath { get; set; }
        public IFormFile? PictureFile { get; set; }
        public byte[]? PictureFlag { get; set; }


    }
}
