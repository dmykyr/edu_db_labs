using System;
using System.Collections.Generic;

namespace DbREstService.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Deadline { get; set; }

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;
}
