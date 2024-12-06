using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitMovie.Business;

public static class CitMovieServiceCollectionExtensions
{
    public static IServiceCollection AddCitMovieServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHelper, PasswordHelper>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<ITitleTypeRepository, TitleTypeRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IReleaseRepository, ReleaseRepository>();
        services.AddScoped<IPromotionalMediaRepository, PromotionalMediaRepository>();
        services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
        services.AddScoped<IBookmarkRepository, BookmarkRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ITitleAttributeRepository, TitleAttributeRepository>();
        services.AddScoped<IUserScoreRepository, UserScoreRepository>();
        services.AddScoped<ICompletedRepository, CompletedRepository>();
        services.AddScoped<ITitleManager, TitleManager>();

        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ILoginManager, LoginManager>();
        services.AddScoped<IMediaManager, MediaManager>();
        services.AddScoped<ITitleTypeManager, TitleTypeManager>();
        services.AddScoped<IPromotionalMediaManager, PromotionalMediaManager>();
        services.AddScoped<ILanguageManager, LanguageManager>();
        services.AddScoped<IJobCategoryManager, JobCategoryManager>();
        services.AddScoped<ICountryManager, CountryManager>();
        services.AddScoped<IGenreManager, GenreManager>();
        services.AddScoped<IFollowManager, FollowManager>();
        services.AddScoped<IReleaseManager, ReleaseManager>();
        services.AddScoped<ISearchHistoryManager, SearchHistoryManager>();
        services.AddScoped<IBookmarkManager, BookmarkManager>();
        services.AddScoped<IPersonManager, PersonManager>();
        services.AddScoped<ITitleAttributeManager, TitleAttributeManager>();
        services.AddScoped<IUserScoreManager, UserScoreManager>();
        services.AddScoped<ICompletedManager, CompletedManager>();
        services.AddScoped<ICrewRepository, CrewRepository>();
        services.AddScoped<ITitleRepository, TitleRepository>();

        services.AddOptions<JwtOptions>()
            .Configure<IConfiguration>((options, configuration) =>
            {
                configuration.Bind("JwtOptions", options);

                // Override with environment variables
                options.Issuer = configuration.GetValue<string>("JWT_ISSUER") ?? options.Issuer;
                options.Audience = configuration.GetValue<string>("JWT_AUDIENCE") ?? options.Audience;
                options.SigningKey = configuration.GetValue<string>("JWT_SIGNING_KEY") ?? options.SigningKey;
            });
        
        services.AddKeyedSingleton<Dictionary<string, RefreshToken>>("refreshTokenCache");
        services.AddScoped<IRefreshTokenCache, RefreshTokenCache>();
        return services;
    }
}