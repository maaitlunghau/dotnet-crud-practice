using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoTask>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<TodoTask>()
            .Property(t => t.Priority)
            .HasConversion<string>();
    }

    public DbSet<TodoTask> Tasks { get; set; }
}
