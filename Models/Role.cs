using System;
using System.Collections.Generic;

namespace Proyecto_Web.Models;

public partial class Role
{
    public int RoleD { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
