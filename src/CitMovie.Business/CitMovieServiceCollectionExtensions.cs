using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitMovie.Business;

public static class CitMovieServiceCollectionExtensions
{
    public static IServiceCollection AddCitMovieServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ILoginManager, LoginManager>();
        
        services.AddScoped<ITitleTypeRepository, TitleTypeRepository>();
        services.AddScoped<ITitleTypeManager, TitleTypeManager>();
        
        services.AddOptions<JwtOptions>()
            .Configure<IConfiguration>((options, configuration) => {
                configuration.Bind("JwtOptions", options);

                // Override with environment variables
                options.Issuer = configuration.GetValue<string>("JWT_ISSUER") ?? options.Issuer;
                options.Audience = configuration.GetValue<string>("JWT_AUDIENCE") ?? options.Audience;
                options.SigningKey = configuration.GetValue<string>("JWT_SIGNING_KEY") ?? options.SigningKey;
            });
        return services;
    }
}