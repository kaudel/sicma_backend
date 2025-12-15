namespace Sicma.Entities;

public partial class UserRecord:BaseEntity
{

    public bool IsVictory { get; set; }
    public bool IsFinished { get; set; }
    public int SuccessScore { get; set; }
    public int FailScore { get; set; } = 0!;
    public int FinalScore { get; set; }
    public Guid UserId { get; set; }
    public Guid PracticeConfigId { get; set; }
    public AppUser AppUser { get; set; }
    public PracticeConfig PracticeConfig { get; set; }

}
