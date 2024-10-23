using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class FrameworkContext : DbContext
{
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Completed> Completeds { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserScore> UserScores { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    
    public FrameworkContext(DbContextOptions<FrameworkContext> options) 
        : base(options) { }

}