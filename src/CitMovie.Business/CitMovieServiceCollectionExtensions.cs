using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CitMovie.Data.Repositories;
using CitMovie.Business.Managers;
using System;

namespace CitMovie.Business
{
    public static class CitMovieServiceCollectionExtensions
    {
        public static IServiceCollection AddCitMovieServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            // Token Generator
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // User Management
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ILoginManager, LoginManager>();

            // Title Type Management
            services.AddScoped<ITitleTypeRepository, TitleTypeRepository>();
            services.AddScoped<ITitleTypeManager, TitleTypeManager>();

            // Language Management
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageManager, LanguageManager>();

            // Bookmark Management
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<IBookmarkManager, BookmarkManager>();

            // JWT Options Configuration
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
}
