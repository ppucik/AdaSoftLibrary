using AdaSoftLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdaSoftLibrary.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Specify DbSet properties
    public virtual DbSet<Book> Books { get; set; } = null!;
    public virtual DbSet<Borrowed> Borroweds { get; set; } = null!;

    // Creating and configuring a Models 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}