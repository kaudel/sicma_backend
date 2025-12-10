using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sicma.Entities;

namespace Sicma.DataAccess.Context;

public partial class DbSicmaContext : IdentityDbContext<AppUser>
{

    public DbSicmaContext(DbContextOptions<DbSicmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OperationConfig> OperationConfigs { get; set; }

    public virtual DbSet<TrainingType> TrainingTypes { get; set; }

    public virtual DbSet<AppUser> Users { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TrainingType>().HasData(
            new TrainingType
            {
                Id = "0af6fbbc-4388-4c4d-a7e0-dff5900b45ca",
                CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4318),
                Name = "Entrenamiento",
                Description = "Opcion entrenamiento",
                CreatedUserId = "1",
                IsActive = true,
            },
            new TrainingType
            {
                Id = "28c52fce-4774-4a1b-8d7c-ff98dd43ff19",
                CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(1971),
                Name = "Torneo",
                Description = "Opcion torneo",
                CreatedUserId = "1",
                IsActive = true,
            },
            new TrainingType
            {
                Id = "78cfc855-13be-4eb3-9d42-bff6c402f479",
                CreatedDate = new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4325),
                Name = "1vs1",
                Description = "Opcion 1 contra 1",
                CreatedUserId = "1",
                IsActive = true,
            }
        );

        modelBuilder.Entity<Exercise>()
            .HasOne(p => p.OperationConfig)
            .WithOne()
            .HasForeignKey<Exercise>(p => p.OperationConfigId);

        modelBuilder.Entity<Exercise>()
            .HasOne( p=>p.TrainingType)
            .WithOne()
            .HasForeignKey<Exercise>( p => p.TrainingTypeId);
    }    
}
