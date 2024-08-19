﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Web.ViewModels.Countries
{
    public class DeleteCountryViewModel : ViewModelBase
    {

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
    }
}
