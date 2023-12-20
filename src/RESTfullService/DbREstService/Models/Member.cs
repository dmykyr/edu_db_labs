using System.ComponentModel.DataAnnotations.Schema;

namespace DbREstService.Models;

public partial class Member
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}