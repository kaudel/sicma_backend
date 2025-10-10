using System;
using System.Collections.Generic;

namespace Sicma.Entities;

public partial class OperationConfig:BaseEntity
{
    public string OperationName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int NumElements { get; set; }

    public int Digits { get; set; }

    public string Range { get; set; } = null!;

}
