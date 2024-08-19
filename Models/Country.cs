using System;
using System.Collections.Generic;

namespace Proyecto_Web.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? Capital { get; set; }

    public string? Population { get; set; }

    public string? Currency { get; set; }

    public string? Continent { get; set; }

    public string? Language { get; set; }

    public byte[]? PictureFlag { get; set; }

    public string? PicturePath { get; set; }
}
