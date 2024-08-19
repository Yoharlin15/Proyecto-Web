using System;
using System.Collections.Generic;

namespace Proyecto_Web.Models;

public partial class YohaNews
{
    public int NewsId { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Description { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public byte[]? Picture { get; set; }

    public string? PicturePath { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
