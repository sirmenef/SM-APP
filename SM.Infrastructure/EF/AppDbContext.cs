using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Infrastructure.Model;

namespace SM.Infrastructure.EF;

public class AppDbContext : IdentityDbContext<User>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.HasDefaultSchema("SM");
    
    builder.Entity<Friends>().HasKey(f => f.Id);
    builder.Entity<Friends>().Property(p => p.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
    builder.Entity<Friends>().Property(p => p.UserId).IsRequired();
    builder.Entity<Friends>().Property(p => p.FriendId).IsRequired();
    builder.Entity<Friends>().HasOne(f => f.User).WithMany(u => u.Following).OnDelete(DeleteBehavior.NoAction);

    builder.Entity<Post>()
      .HasOne(u => u.User)
      .WithMany(p => p.Posts)
      .HasForeignKey(p => p.UserId)
      .IsRequired();

    builder.Entity<Post>(eb =>
    {
      eb.HasKey(e => e.PostId);
      eb.Property(e => e.PostId).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      eb.Property(p => p.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
      eb.Property(p => p.Message).HasMaxLength(140);
    });
    
    builder.Entity<Likes>(eb =>
    {
      eb.HasKey(e => e.Id);
      eb.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      eb.Property(p => p.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
    });
  }

  public DbSet<Friends> Friends { get; set; }
  public DbSet<Post> Posts { get; set; }
  public DbSet<Likes> Likes { get; set; }
}