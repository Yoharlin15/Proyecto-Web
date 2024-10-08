﻿using System;
using System.Collections.Generic;

namespace Proyecto_Web.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<YohaNews> YohaNews { get; set; } = new List<YohaNews>();
}
