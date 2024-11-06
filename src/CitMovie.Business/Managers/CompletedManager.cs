namespace CitMovie.Business;

public class CompletedManager : ICompletedManager
{
    private readonly ICompletedRepository _completedRepository;

    public CompletedManager(ICompletedRepository completedRepository) =>
        _completedRepository = completedRepository;

    public async Task<CompletedDto> MoveBookmarkToCompletedAsync(int userId, int mediaId, int rewatchability, string? note = null)
    {
        var completed = await _completedRepository.MoveBookmarkToCompletedAsync(userId, mediaId, rewatchability, note);

        // Map to CompletedDto
        return new CompletedDto
        {
            CompletedId = completed.CompletedId,
            UserId = completed.UserId,
            MediaId = completed.MediaId,
            CompletedDate = completed.CompletedDate,
            Rewatchability = completed.Rewatchability,
            Note = completed.Note
        };
    }

    public async Task<CompletedDto?> GetCompletedAsync(int completedId)
    {
        var completed = await _completedRepository.GetCompletedByIdAsync(completedId);
        return completed == null ? null : MapToDto(completed);
    }

    public async Task<IEnumerable<CompletedDto>> GetUserCompletedItemsAsync(int userId, int page, int pageSize)
    {
        var completedItems = await _completedRepository.GetUserCompletedItemsAsync(userId, page, pageSize);
        return completedItems.Select(MapToDto);
    }

    public async Task<CompletedDto?> UpdateCompletedAsync(int completedId, UpdateCompletedDto updateCompletedDto)
    {
        var completed = await _completedRepository.GetCompletedByIdAsync(completedId);
        if (completed == null) return null;

        if (updateCompletedDto.CompletedDate.HasValue)
            completed.CompletedDate = updateCompletedDto.CompletedDate;

        if (updateCompletedDto.Rewatchability.HasValue)
            completed.Rewatchability = updateCompletedDto.Rewatchability.Value;

        if (updateCompletedDto.Note != null)
            completed.Note = updateCompletedDto.Note;

        var updated = await _completedRepository.UpdateCompletedAsync(completed);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteCompletedAsync(int completedId) =>
        await _completedRepository.DeleteCompletedAsync(completedId);

    private static CompletedDto MapToDto(Completed completed) =>
        new()
        {
            CompletedId = completed.CompletedId,
            UserId = completed.UserId,
            MediaId = completed.MediaId,
            CompletedDate = completed.CompletedDate,
            Rewatchability = completed.Rewatchability,
            Note = completed.Note
        };

    public async Task<int> GetTotalUserCompletedCountAsync(int userId)
    {
        return await _completedRepository.GetTotalUserCompletedCountAsync(userId);
    }
}
