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

    //public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        base.OnModelCreating(modelBuilder);
    }    
}
