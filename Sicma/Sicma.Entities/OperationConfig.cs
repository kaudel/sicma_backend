using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities;

public  class OperationConfig:BaseEntity
{
    [MaxLength(100)]
    public string OperationName { get; set; } = null!;
    [MaxLength(600)]
    public string Description {  get; set; } = null!;
    [MaxLength(2)]
    public string TypeSimbol { get; set; } = null!;
    public int NumElements { get; set; }
    public int Digits { get; set; }
    [MaxLength(30)]
    public string Range { get; set; } = null!;
    [MaxLength(100)]
    public string RegexExpression { get; set; } = null!;
    public int TimePeriodSeconds {  get; set; }
}
