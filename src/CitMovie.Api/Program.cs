using CitMovie.Business;
using CitMovie.Data;
using CitMovie.Data.FollowRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FrameworkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<FollowService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

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