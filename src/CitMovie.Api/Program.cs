using CitMovie.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FrameworkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));;

builder.Services.AddCitMovieServices();

builder.Services.AddControllers();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 5001;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

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
