using System.ComponentModel.DataAnnotations.Schema;

namespace DbREstService.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int CardNumber { get; set; }

    public int CardCvv { get; set; }

    public DateTime CardExpireDate { get; set; }

    public string Email { get; set; } = null!;

    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}