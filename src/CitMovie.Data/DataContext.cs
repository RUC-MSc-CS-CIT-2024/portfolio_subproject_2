using Microsoft.Extensions.DependencyInjection;

namespace CitMovie.Data;

public class DataContext : DbContext
{
    private readonly string? _connectionString;

    public DbSet<CastMember> CastMembers { get; set; }
    public DbSet<MediaCollection> Collections { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CrewMember> CrewMembers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<MediaProductionCompany> MediaProductionCompanies { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<PromotionalMedia> PromotionalMedia { get; set; }
    public DbSet<RelatedMedia> RelatedMedia { get; set; }
    public DbSet<Release> Releases { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleAttribute> TitleAttributes { get; set; }
    public DbSet<TitleType> TitleTypes { get; set; }
    public DbSet<CoActor> CoActors { get; set; }

    public DataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    [ActivatorUtilitiesConstructor]
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _connectionString != null)
            optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaProductionCompany>()
            .HasKey(od => new { od.MediaId, od.ProductionCompanyId });

        modelBuilder.Entity<Media>().UseTptMappingStrategy();

        modelBuilder.Entity<Media>()
            .HasMany(x => x.RelatedMedia)
            .WithOne(x => x.Primary)
            .HasForeignKey(x => x.PrimaryId);
        
        modelBuilder.HasDbFunction(() => ExactMatchSearch(default!, default!))
            .HasName("exact_match_titles");
        modelBuilder.HasDbFunction(() => BestMatchSearch(default!, default!))
            .HasName("best_match_titles");
        modelBuilder.HasDbFunction(() => SimpleSearch(default!, default!))
            .HasName("simple_search");
        modelBuilder.HasDbFunction(() => StructuredSearch(default, default!, default!, default!, default!))
            .HasName("structured_string_search");
        modelBuilder.HasDbFunction(() => GetSimilarMedia(default))
            .HasName("get_similar_movies");
        modelBuilder.HasDbFunction(() => GetFrequentCoActors(default!))
             .HasName("get_frequent_coplaying_actors");

        modelBuilder.Entity<Media>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Media)
            .UsingEntity<Dictionary<string, object>>("media_genre", 
                r => r.HasOne<Genre>().WithMany().HasForeignKey("genre_id"),
                l => l.HasOne<Media>().WithMany().HasForeignKey("media_id"));

        modelBuilder.Entity<Media>()
            .HasMany(e => e.Countries)
            .WithMany(e => e.Media)
            .UsingEntity<Dictionary<string, object>>("media_production_country", 
                r => r.HasOne<Country>().WithMany().HasForeignKey("country_id"),
                l => l.HasOne<Media>().WithMany().HasForeignKey("media_id"));
        
        modelBuilder.Entity<TitleAttribute>()
            .HasMany(r => r.Titles)
            .WithMany(l => l.TitleAttributes)
            .UsingEntity<Dictionary<string, object>>(
                "title_title_attribute",
                r => r.HasOne<Title>().WithMany().HasForeignKey("title_id"),
                l => l.HasOne<TitleAttribute>().WithMany().HasForeignKey("title_attribute_id"));

        modelBuilder.Entity<TitleType>()
            .HasMany(r => r.Titles)
            .WithMany(l => l.TitleTypes)
            .UsingEntity<Dictionary<string, object>>(
                "title_title_type",
                r => r.HasOne<Title>().WithMany().HasForeignKey("title_id"),
                l => l.HasOne<TitleType>().WithMany().HasForeignKey("title_type_id"));

        modelBuilder.Entity<Release>()
            .HasMany(r => r.SpokenLanguages)
            .WithMany(l => l.Releases)
            .UsingEntity<Dictionary<string, object>>(
                "spoken_language",
                r => r.HasOne<Language>().WithMany().HasForeignKey("language_id"),
                l => l.HasOne<Release>().WithMany().HasForeignKey("release_id"));
    }

    public IQueryable<MatchSearchResult> ExactMatchSearch(string[] keywords, int? userId)
        => FromExpression(() => ExactMatchSearch(keywords, userId));
    
    public IQueryable<MatchSearchResult> BestMatchSearch(string[] keywords, int? userId)
        => FromExpression(() => BestMatchSearch(keywords, userId));
    
    public IQueryable<MatchSearchResult> SimpleSearch(string query, int? userId)
        => FromExpression(() => SimpleSearch(query, userId));

    public IQueryable<MatchSearchResult> StructuredSearch(string? title, string? plot, string? character, string? person, int? userId)
        => FromExpression(() => StructuredSearch(title, plot, character, person, userId));

    public IQueryable<MatchSearchResult> GetSimilarMedia(int id)
        => FromExpression(() => GetSimilarMedia(id));

    public IQueryable<CoActor> GetFrequentCoActors(string actorName)
        => FromExpression(() => GetFrequentCoActors(actorName));
}