namespace DbREstService.Models;

public partial class ProjectsMember
{
    public int MemberId { get; set; }

    public int ProjectId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
