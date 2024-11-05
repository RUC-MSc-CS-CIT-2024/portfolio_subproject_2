using Microsoft.Extensions.DependencyInjection;

namespace CitMovie.Data;

public class FrameworkContext : DbContext
{
    private readonly string? _connectionString;

    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Completed> Completed { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserScore> UserScores { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }

    public FrameworkContext(string connectionString) {
        _connectionString = connectionString;
    }

    [ActivatorUtilitiesConstructor]
    public FrameworkContext(DbContextOptions<FrameworkContext> options) 
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured && _connectionString != null)
            optionsBuilder.UseNpgsql(_connectionString);
    }
}
