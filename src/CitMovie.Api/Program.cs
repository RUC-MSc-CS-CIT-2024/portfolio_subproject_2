using System.Security.Claims;
using System.Text;
using CitMovie.Data;
using CitMovie.Data.FollowRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FrameworkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));;


builder.Services.AddControllers();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<FollowService>();

builder.Services.AddCitMovieServices();

builder.Services.AddControllers();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 5001;
});



builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        JwtOptions jwtSettings = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
            ?? new JwtOptions() {
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

builder.Services.AddAuthorization(options => {
    options.AddPolicy("user_scope", policy => {
        policy.RequireClaim("user_id");
        policy.RequireAssertion(context => {
            string? userId = (context.Resource as HttpContext)?.Request.RouteValues["userId"]?.ToString();
            return userId != null && context.User.FindFirstValue("user_id") == userId;
        });
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseHsts();
}

app.MapControllers();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/test-connection", async (FrameworkContext context) =>
{
    try
    {
        var userCount = await context.Users.CountAsync();
        return Results.Ok($"Connection successful! User count: {userCount}");
    }
    catch (Exception ex)
    {
        return Results.Problem("Connection test failed: " + ex.Message);
    }
});

app.Run();