using System;
using System.Collections.Generic;

namespace DbREstService.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int CardNumber { get; set; }

    public int CardCvv { get; set; }

    public DateTime CardExpireDate { get; set; }

    public string Email { get; set; } = null!;

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;
}
