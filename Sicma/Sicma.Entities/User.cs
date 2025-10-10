namespace Sicma.Entities;

public partial class User:BaseEntity
{
    public string Nickname { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Institution { get; set; } = null!;

    public int UserTypeId { get; set; }

    public virtual UserType UserType { get; set; } = null!;
}
