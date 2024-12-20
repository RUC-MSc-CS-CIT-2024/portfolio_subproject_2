using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using CitMovie.Api;
using CitMovie.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Basic",
        Name = "Basic login"
    });
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme() {
                Reference = new() { Type = ReferenceType.SecurityScheme, Id = "basic" }
            },
            []
        },
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            },
            []
        }
    });
});

builder.Services.AddDbContext<FrameworkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PagingHelper>();

builder.Services.AddCitMovieServices();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 5001;
});



builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        JwtOptions jwtSettings = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
            ?? new JwtOptions()
            {
                Issuer = builder.Configuration.GetValue<string>("JWT_ISSUER") ?? throw new Exception("JWT Issuer not found"),
                Audience = builder.Configuration.GetValue<string>("JWT_AUDIENCE") ?? throw new Exception("JWT Audience not found"),
                SigningKey = builder.Configuration.GetValue<string>("JWT_SIGNING_KEY") ?? throw new Exception("JWT Signing Key not found")
            };
        jwtSettings.Issuer = builder.Configuration.GetValue<string>("JWT_ISSUER") ?? jwtSettings.Issuer;
        jwtSettings.Audience = builder.Configuration.GetValue<string>("JWT_AUDIENCE") ?? jwtSettings.Audience;
        jwtSettings.SigningKey = builder.Configuration.GetValue<string>("JWT_SIGNING_KEY") ?? jwtSettings.SigningKey;

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user_scope", policy =>
    {
        policy.RequireClaim("user_id");
        policy.RequireAssertion(context =>
        {
            HttpContext? httpContext = context.Resource as HttpContext;
            object? userIdRoute = httpContext?.Request.RouteValues["userId"];
            if (userIdRoute is null)
                return true;
            string? userId = userIdRoute.ToString();
            return userId != null && context.User.FindFirstValue("user_id") == userId;
        });
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseCors();
app.MapControllers();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();