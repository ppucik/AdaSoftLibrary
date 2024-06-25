using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdaSoftLibrary.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDataContext
{
    private readonly ILogger<AppDbContext> _logger;

    public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    public Library Library { get; init; } = new();

    // Specify DbSet properties
    public virtual DbSet<Book> Books { get; set; } = null!;
    public virtual DbSet<Borrowed> Borroweds { get; set; } = null!;

    // Creating and configuring a Models
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}