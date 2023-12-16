using System;
using System.Collections.Generic;

namespace DbREstService.Models;

public partial class RoleGrant
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
