using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CitMovie.Data;

public class FrameworkContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        }
    }

    public void TestConnection()
    {
        try
        {
            var userCount = Users.Count();
            Console.WriteLine($"User count: {userCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Connection test failed: " + ex.Message);
        }
    }


    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Completed> Completeds { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserScore> UserScores { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    
    public FrameworkContext(DbContextOptions<FrameworkContext> options) 
        : base(options) { }

}
