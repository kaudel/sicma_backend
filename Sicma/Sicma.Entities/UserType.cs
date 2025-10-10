using System;
using System.Collections.Generic;

namespace Sicma.Entities;

public partial class UserType: BaseEntity
{
    public string UserTypeName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
