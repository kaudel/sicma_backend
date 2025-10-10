using System;
using System.Collections.Generic;

namespace Sicma.Entities;

public partial class PracticeLevel: BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
