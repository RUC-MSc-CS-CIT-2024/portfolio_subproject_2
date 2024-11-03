using CitMovie.Api.Controllers;
using CitMovie.Business.Managers;
using CitMovie.Data;
using CitMovie.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public class BookmarkControllerIntegrationTests
{
    private readonly BookmarkController _controller;
    private readonly BookmarkManager _bookmarkManager;
    private readonly FrameworkContext _context;

    public BookmarkControllerIntegrationTests()
    {
        // Set up in-memory database and services
        var services = new ServiceCollection();
        services.AddDbContext<FrameworkContext>(options => options.UseInMemoryDatabase("TestDb"));
        services.AddScoped<IBookmarkRepository, BookmarkRepository>();
        services.AddScoped<IBookmarkManager, BookmarkManager>();

        var provider = services.BuildServiceProvider();
        _context = provider.GetRequiredService<FrameworkContext>();
        var bookmarkManager = provider.GetRequiredService<IBookmarkManager>();

        // Initialize the controller with the manager
        _controller = new BookmarkController(bookmarkManager);
    }

    [Fact]
    public async Task CreateBookmark_ShouldSucceed_WhenDataIsValid()
    {
        var createBookmarkDto = new CreateBookmarkDto { MediaId = 101, Note = "Great Movie" };
        var result = await _controller.CreateBookmark(createBookmarkDto);
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(createdResult.Value);
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnBookmark_WhenExists()
    {
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "Another Movie" };
        var createdBookmark = await _controller.CreateBookmark(createDto);
        var createdActionResult = createdBookmark as CreatedAtActionResult;
        var createdId = (createdActionResult?.Value as BookmarkDto)?.BookmarkId;

        var result = await _controller.GetBookmark((int)createdId);
        Assert.IsType<OkObjectResult>(result);
    }
}
