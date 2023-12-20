using System.ComponentModel.DataAnnotations.Schema;

namespace DbREstService.Models;

public partial class Review
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int Rate { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}