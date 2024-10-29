using CitMovie.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitMovie.Business.Managers
{
    public interface IBookmarkManager
    {
        /// <summary>
        /// Creates a new bookmark for a user.
        /// </summary>
        /// <param name="createBookmarkDto">The DTO containing the bookmark details.</param>
        /// <returns>The created bookmark as a BookmarkDto.</returns>
        Task<BookmarkDto> CreateBookmarkAsync(CreateBookmarkDto createBookmarkDto);

        /// <summary>
        /// Retrieves a specific bookmark by its ID.
        /// </summary>
        /// <param name="bookmarkId">The unique ID of the bookmark.</param>
        /// <returns>The bookmark as a BookmarkDto if found, otherwise null.</returns>
        Task<BookmarkDto> GetBookmarkAsync(int bookmarkId);

        /// <summary>
        /// Retrieves all bookmarks for a specified user.
        /// </summary>
        /// <param name="userId">The user ID whose bookmarks are being retrieved.</param>
        /// <returns>A collection of bookmarks as BookmarkDto objects.</returns>
        Task<IEnumerable<BookmarkDto>> GetUserBookmarksAsync(int userId);

        /// <summary>
        /// Updates an existing bookmark.
        /// </summary>
        /// <param name="bookmarkId">The ID of the bookmark to update.</param>
        /// <param name="note">The new note content for the bookmark, if any.</param>
        /// <returns>The updated bookmark as a BookmarkDto if successful, otherwise null.</returns>
        Task<BookmarkDto> UpdateBookmarkAsync(int bookmarkId, string? note);

        /// <summary>
        /// Deletes a specific bookmark by ID.
        /// </summary>
        /// <param name="bookmarkId">The unique ID of the bookmark to delete.</param>
        /// <returns>True if the bookmark was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteBookmarkAsync(int bookmarkId);
    }
}
