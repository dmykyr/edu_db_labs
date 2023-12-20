namespace DbREstService.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}