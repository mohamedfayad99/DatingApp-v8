using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    public DbSet<UserLike> LikedUsers{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserLike>().HasKey(x => new {x.sourceuserId ,x.targetuserId});

        builder.Entity<UserLike>().HasOne(s=>s.SourceUser)
        .WithMany(s=>s.LikedUsers)
        .HasForeignKey(s=>s.sourceuserId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserLike>().HasOne(s=>s.TargetUser)
        .WithMany(s=>s.LikeByUsers)
        .HasForeignKey(s=>s.targetuserId).OnDelete(DeleteBehavior.Cascade);
    }

}
