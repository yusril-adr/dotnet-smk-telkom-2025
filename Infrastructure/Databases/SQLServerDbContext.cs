using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Infrastructure.Databases;

public class SQLServerDBContext(
  DbContextOptions<SQLServerDBContext> options
) : DbContext(options)
{
  // Register the entities/models
  public DbSet<User> Users { get; set; }
  public DbSet<Post> Posts { get; set; }
  public DbSet<PostComment> PostComments { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    GenerateUuid<User>(modelBuilder, "Id");
    GenerateUuid<Post>(modelBuilder, "Id");
    GenerateUuid<PostComment>(modelBuilder, "Id");

    modelBuilder.Entity<User>()
      .HasIndex(u => u.Email)
      .IsUnique();

    /* Case:
      When a User is deleted → all comments of that user must be deleted first.
      When a Post is deleted → all its comments are deleted.
    */
    modelBuilder.Entity<PostComment>()
        .HasOne(pc => pc.AuthorUser) // Author
        .WithMany(u => u.PostComments)
        .HasForeignKey(pc => pc.AuthorUserId)
        .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

    modelBuilder.Entity<PostComment>()
        .HasOne(pc => pc.Post)
        .WithMany(p => p.Comments)
        .HasForeignKey(pc => pc.PostId)
        .OnDelete(DeleteBehavior.Cascade); // only allow cascade from Post
  }

  public override int SaveChanges()
  {
    var currentTime = DateTime.Now;

    var entries = ChangeTracker
        .Entries<Base>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    foreach (var entry in entries)
    {
      var entity = entry.Entity;

      if (entry.State == EntityState.Added)
      {
        entity.CreatedAt = currentTime;
        entity.UpdatedAt = currentTime;
      }
      else if (entry.State == EntityState.Modified)
      {
        entity.UpdatedAt = currentTime;
        entry.Property(nameof(entity.CreatedAt)).IsModified = false;
      }
    }

    return base.SaveChanges();
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var currentTime = DateTime.Now;

    var entries = ChangeTracker
        .Entries<Base>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    foreach (var entry in entries)
    {
      var entity = entry.Entity;

      if (entry.State == EntityState.Added)
      {
        entity.CreatedAt = currentTime;
        entity.UpdatedAt = currentTime;
      }
      else if (entry.State == EntityState.Modified)
      {
        entity.UpdatedAt = currentTime;
        entry.Property(nameof(entity.CreatedAt)).IsModified = false;
      }
    }

    return base.SaveChangesAsync(cancellationToken);
  }

  /*=================================== Service Support ===========================================*/

  private static void GenerateUuid<T>(ModelBuilder modelBuilder, string column) where T : class
  {
    modelBuilder.Entity<T>()
        .HasIndex(CreateExpression<T>(column));

    modelBuilder.Entity<T>()
        .Property(CreateExpression<T>(column))
        .HasDefaultValueSql("NEWID()");
  }

  private static Expression<Func<T, object>> CreateExpression<T>(string uuid) where T : class
  {
    var type = typeof(T);
    var property = type.GetProperty(uuid);
    var parameter = Expression.Parameter(type);
    var access = Expression.Property(parameter, property);
    var convert = Expression.Convert(access, typeof(object));
    var function = Expression.Lambda<Func<T, object>>(convert, parameter);

    return function;
  }
}
