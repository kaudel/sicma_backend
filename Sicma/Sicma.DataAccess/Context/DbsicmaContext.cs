using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sicma.Entities;

namespace Sicma.DataAccess.Context;

public partial class DbSicmaContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public DbSicmaContext(DbContextOptions<DbSicmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OperationConfig> OperationConfigs { get; set; }

    public virtual DbSet<TrainingType> TrainingTypes { get; set; }

    public virtual DbSet<AppUser> Users { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<TokenHistory> TokenHistory { get; set; }

    public virtual DbSet<PracticeConfig> PracticeConfigs { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<UserRecord> UserRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<Institution>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<OperationConfig>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<TrainingType>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<PracticeConfig>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");
        
        modelBuilder.Entity<Classroom>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<TokenHistory>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<UserRecord>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");


        modelBuilder.Entity<PracticeConfig>()
            .HasOne(p => p.OperationConfig)
            .WithOne()
            .HasForeignKey<PracticeConfig>(p => p.OperationConfigId);

        modelBuilder.Entity<PracticeConfig>()
            .HasOne(p => p.TrainingType)
            .WithOne()
            .HasForeignKey<PracticeConfig>(p => p.TrainingTypeId);

        //computed field
        modelBuilder.Entity<TokenHistory>()
            .Property(p => p.IsActive)
            .HasComputedColumnSql("Case WHEN [ExpirationDate] > GETDATE() THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END");

        modelBuilder.Entity<Classroom>()
            .HasOne(p => p.Institution)
            .WithOne()
            .HasForeignKey<Classroom>(p => p.InstitutionId);

        modelBuilder.Entity<Classroom>()
            .HasOne(p => p.PracticeConfig)
            .WithOne()
            .HasForeignKey<Classroom>(p => p.PracticeConfigId);

        modelBuilder.Entity<UserRecord>()
            .HasOne(p => p.AppUser)
            .WithOne()
            .HasForeignKey<UserRecord>(p => p.UserId);

        modelBuilder.Entity<UserRecord>()
            .HasOne(p => p.PracticeConfig)
            .WithOne()
            .HasForeignKey<UserRecord>(p => p.PracticeConfigId);

        modelBuilder.Entity<TrainingType>().HasData(
    new TrainingType
    {
        Id = Guid.Parse("0af6fbbc-4388-4c4d-a7e0-dff5900b45ca"),
        CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4318),
        Name = "Entrenamiento",
        Description = "Opcion entrenamiento",
        CreatedUserId = Guid.Parse("b7c6a2e4-1d6f-4a9c-9b3c-9d5d91c5a111"),
        IsActive = true
    },
    new TrainingType
    {
        Id = Guid.Parse("28c52fce-4774-4a1b-8d7c-ff98dd43ff19"),
        CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(1971),
        Name = "Torneo",
        Description = "Opcion torneo",
        CreatedUserId = Guid.Parse("b7c6a2e4-1d6f-4a9c-9b3c-9d5d91c5a111"),
        IsActive = true,
    },
    new TrainingType
    {
        Id = Guid.Parse("78cfc855-13be-4eb3-9d42-bff6c402f479"),
        CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4325),
        Name = "1vs1",
        Description = "Opcion 1 contra 1",
        CreatedUserId = Guid.Parse("b7c6a2e4-1d6f-4a9c-9b3c-9d5d91c5a111"),
        IsActive = true,
    }
);
    }
}
