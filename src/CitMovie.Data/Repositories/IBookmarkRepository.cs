namespace CitMovie.Data;
public interface IBookmarkRepository
{
    /// <summary>
    /// Adds a new bookmark to the database.
    /// </summary>
    /// <param name="bookmark">The bookmark entity to add.</param>
    /// <returns>The added bookmark entity.</returns>
    Task<Bookmark> AddBookmarkAsync(Bookmark bookmark);

    /// <summary>
    /// Retrieves a bookmark by its unique ID without tracking.
    /// </summary>
    /// <param name="bookmarkId">The ID of the bookmark to retrieve.</param>
    /// <returns>The bookmark entity if found; otherwise, null.</returns>
    Task<Bookmark> GetBookmarkByIdAsync(int bookmarkId);

    /// <summary>
    /// Retrieves all bookmarks for a specific user without tracking.
    /// </summary>
    /// <param name="userId">The ID of the user whose bookmarks are retrieved.</param>
    /// <returns>A collection of bookmark entities for the specified user.</returns>
    Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId);

    /// <summary>
    /// Updates an existing bookmark in the database.
    /// Only saves if there is a change to avoid unnecessary database calls.
    /// </summary>
    /// <param name="bookmark">The bookmark entity with updated information.</param>
    /// <returns>The updated bookmark entity if successful; otherwise, null.</returns>
    Task<Bookmark> UpdateBookmarkAsync(Bookmark bookmark);

    /// <summary>
    /// Deletes a bookmark by its unique ID.
    /// </summary>
    /// <param name="bookmarkId">The ID of the bookmark to delete.</param>
    /// <returns>True if the bookmark was successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteBookmarkAsync(int bookmarkId);
}