using System.ComponentModel.DataAnnotations.Schema;

namespace DbREstService.Models;
public partial class RoleGrant
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}