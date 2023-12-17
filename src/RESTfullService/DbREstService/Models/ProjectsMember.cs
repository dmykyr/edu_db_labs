using System.ComponentModel.DataAnnotations.Schema;

namespace DbREstService.Models;

public partial class ProjectsMember
{
    public int MemberId { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey("MemberId")]
    public virtual Member Member { get; set; } = null!;

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}
