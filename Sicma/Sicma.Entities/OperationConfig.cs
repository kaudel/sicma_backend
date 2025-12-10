namespace Sicma.Entities;

public  class OperationConfig:BaseEntity
{
    public string OperationName { get; set; } = null!;
    public string Description {  get; set; } = null!;

    public string TypeSimbol { get; set; } = null!;

    public int NumElements { get; set; }

    public int Digits { get; set; }

    public string Range { get; set; } = null!;

    public string RegexExpression { get; set; } = null!;

    public int TimePeriodSeconds {  get; set; }

}
