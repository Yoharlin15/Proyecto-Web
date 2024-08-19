using System;
using System.Collections.Generic;

namespace Proyecto_Web.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? Name { get; set; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? PublicationDate { get; set; }

    public byte[]? Picture { get; set; }

    public string? PicturePath { get; set; }
}
