namespace Sicma.DTO.Request.UserRecord
{
    public class UserRecordRequest
    {
        public bool IsVictory { get; set; }
        public bool IsFinished { get; set; }
        public int SuccessScore { get; set; }
        public int FailScore { get; set; } = 0!;
        public int FinalScore { get; set; }
        public Guid PracticeConfigId { get; set; }
    }
}
