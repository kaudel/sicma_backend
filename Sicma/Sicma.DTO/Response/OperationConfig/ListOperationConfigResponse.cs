namespace Sicma.DTO.Response.OperationConfig
{
    public class ListOperationConfigResponse
    {
        public Guid Id { get; set; }
        public string? OperationName { get; set; }
        public string? Description { get; set; }
        public string? TypeSimbol { get; set; }
        public int NumElements { get; set; }
        public int Digits { get; set; }
        public string? Range { get; set; }
        public string? RegexExpression { get; set; }
        public int TimePeriodSeconds { get; set; }
    }
}
