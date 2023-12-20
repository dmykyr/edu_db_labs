namespace DbREstService.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}