using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class DataContext : DbContext
{
    public DbSet<CastMember> CastMembers { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CrewMember> CrewMembers { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<MediaProductionCompany> MediaProductionCompanies { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<PromotionalMedia> PromotionalMedia { get; set; }
    public DbSet<RelatedMedia> RelatedMedia { get; set; }
    public DbSet<Release> Releases { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleAttribute> TitleAttributes { get; set; }
    public DbSet<TitleType> TitleTypes { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options) { }

}
