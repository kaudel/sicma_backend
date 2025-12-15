namespace Sicma.DTO.Response.UserRecord
{
    public class UserRecordResponse
    {
        public Guid Id { get; set; }
        public bool IsVictory { get; set; }
        public bool IsFinished { get; set; }
        public int SuccessScore { get; set; }
        public int FailScore { get; set; } = 0!;
        public int FinalScore { get; set; }
    }
}
