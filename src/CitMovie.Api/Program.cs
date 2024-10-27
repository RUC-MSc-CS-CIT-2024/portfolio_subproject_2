using CitMovie.Business;
using CitMovie.Data;
using CitMovie.Data.JobCategoryRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FrameworkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))); ;

builder.Services.AddControllers();
builder.Services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
builder.Services.AddScoped<JobCategoryService>();

var app = builder.Build();

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
