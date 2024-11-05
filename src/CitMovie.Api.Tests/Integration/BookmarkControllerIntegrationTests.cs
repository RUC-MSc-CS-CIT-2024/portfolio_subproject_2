using System.Security.Claims;
using CitMovie.Api;
using CitMovie.Business;
using CitMovie.Data;
using CitMovie.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class BookmarkControllerIntegrationTests
{
    private readonly BookmarkController _controller;
    private readonly ServiceProvider _serviceProvider;

    public BookmarkControllerIntegrationTests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<FrameworkContext>(options => options.UseInMemoryDatabase("TestDb"));
        services.AddScoped<IBookmarkRepository, BookmarkRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookmarkManager, BookmarkManager>();

        _serviceProvider = services.BuildServiceProvider();

        var bookmarkManager = _serviceProvider.GetRequiredService<IBookmarkManager>();
        _controller = new BookmarkController(bookmarkManager)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "1") })) }
            }
        };
    }

    [Fact]
    public async Task CreateBookmark_ShouldReturnCreated_WhenValidDataIsProvided()
    {
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "A great movie!" };

        var result = await _controller.CreateBookmark(1, createDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(createdResult.Value);
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnOk_WhenBookmarkExists()
    {
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "Must-watch" };
        var createdResult = await _controller.CreateBookmark(1, createDto) as CreatedAtActionResult;
        var createdId = (createdResult?.Value as BookmarkDto)?.BookmarkId;

        var result = await _controller.GetBookmark(1, createdId ?? 0);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBookmark_ShouldReturnNoContent_WhenBookmarkDeleted()
    {
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "One-time watch" };
        var createdResult = await _controller.CreateBookmark(1, createDto) as CreatedAtActionResult;
        var createdId = (createdResult?.Value as BookmarkDto)?.BookmarkId;

        if (createdId == null)
        {
            throw new InvalidOperationException("Bookmark ID cannot be null");
        }
        var deleteResult = await _controller.DeleteBookmark(1, (int)createdId);

        Assert.IsType<NoContentResult>(deleteResult);
    }
}
