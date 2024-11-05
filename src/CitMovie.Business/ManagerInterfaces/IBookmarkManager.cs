namespace CitMovie.Business;

public interface IBookmarkManager
{
    /// <summary>
    /// Creates a new bookmark for a user.
    /// </summary>
    /// <param name="createBookmarkDto">The DTO containing details for the new bookmark.</param>
    /// <returns>The created bookmark as a BookmarkDto.</returns>
    Task<BookmarkResult> CreateBookmarkAsync(int userId, BookmarkCreateRequest createBookmarkDto);

    /// <summary>
    /// Retrieves a specific bookmark by its ID.
    /// </summary>
    /// <param name="bookmarkId">The unique ID of the bookmark.</param>
    /// <returns>A BookmarkDto representing the bookmark if found; otherwise, null.</returns>
    Task<BookmarkResult> GetBookmarkAsync(int bookmarkId);
    /// <summary>
    /// Retrieves all bookmarks for a specified user.
    /// </summary>
    /// <param name="userId">The ID of the user whose bookmarks are being retrieved.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page for pagination.</param>
    /// <returns>A collection of BookmarkDto objects for the specified user.</returns>
    Task<IEnumerable<BookmarkResult>> GetUserBookmarksAsync(int userId, int page, int pageSize);

    /// <summary>
    /// Updates an existing bookmark's note.
    /// </summary>
    /// <param name="bookmarkId">The ID of the bookmark to update.</param>
    /// <param name="note">The new note content for the bookmark, if any.</param>
    /// <returns>The updated BookmarkDto if successful; otherwise, null.</returns>
    Task<BookmarkResult> UpdateBookmarkAsync(int bookmarkId, string? note);

    /// <summary>
    /// Deletes a specific bookmark by ID.
    /// </summary>
    /// <param name="bookmarkId">The unique ID of the bookmark to delete.</param>
    /// <returns>True if the bookmark was successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteBookmarkAsync(int bookmarkId);

    /// <summary>
    /// Gets the total count of bookmarks for a specified user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The total count of bookmarks for the user.</returns>
    Task<int> GetTotalUserBookmarksCountAsync(int userId);
}
