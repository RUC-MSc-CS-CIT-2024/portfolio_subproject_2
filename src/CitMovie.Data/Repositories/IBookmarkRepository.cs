using CitMovie.Models.DomainObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitMovie.Data.Repositories
{
    public interface IBookmarkRepository
    {
        /// <summary>
        /// Adds a new bookmark to the database.
        /// </summary>
        /// <param name="bookmark">The bookmark entity to add.</param>
        /// <returns>The added bookmark entity.</returns>
        Task<Bookmark> AddBookmarkAsync(Bookmark bookmark);

        /// <summary>
        /// Retrieves a bookmark by its ID.
        /// </summary>
        /// <param name="bookmarkId">The unique ID of the bookmark.</param>
        /// <returns>The matching bookmark entity, or null if not found.</returns>
        Task<Bookmark> GetBookmarkByIdAsync(int bookmarkId);

        /// <summary>
        /// Retrieves all bookmarks for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose bookmarks are being retrieved.</param>
        /// <returns>A collection of bookmarks for the specified user.</returns>
        Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId);

        /// <summary>
        /// Updates an existing bookmark.
        /// </summary>
        /// <param name="bookmark">The bookmark entity with updated information.</param>
        /// <returns>The updated bookmark entity.</returns>
        Task<Bookmark> UpdateBookmarkAsync(Bookmark bookmark);

        /// <summary>
        /// Deletes a bookmark by ID.
        /// </summary>
        /// <param name="bookmarkId">The unique ID of the bookmark to delete.</param>
        /// <returns>True if the bookmark was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteBookmarkAsync(int bookmarkId);
    }
}
