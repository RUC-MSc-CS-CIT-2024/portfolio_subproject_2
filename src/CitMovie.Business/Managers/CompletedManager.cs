namespace CitMovie.Business;

public class CompletedManager : ICompletedManager
{
    private readonly ICompletedRepository _completedRepository;

    public CompletedManager(ICompletedRepository completedRepository) =>
        _completedRepository = completedRepository;

    public async Task<CompletedResult> MoveBookmarkToCompletedAsync(CompletedCreateRequest createCompletedDto)
    {
        var completed = await _completedRepository.MoveBookmarkToCompletedAsync(createCompletedDto.UserId, createCompletedDto.MediaId, createCompletedDto.Rewatchability, createCompletedDto.Note);

        return new CompletedResult
        {
            CompletedId = completed.CompletedId,
            UserId = completed.UserId,
            MediaId = completed.MediaId,
            CompletedDate = completed.CompletedDate,
            Rewatchability = completed.Rewatchability,
            Note = completed.Note
        };
    }

    public async Task<CompletedResult?> GetCompletedAsync(int completedId)
    {
        var completed = await _completedRepository.GetCompletedByIdAsync(completedId);
        return completed == null ? null : MapToDto(completed);
    }

    public async Task<IEnumerable<CompletedResult>> GetUserCompletedItemsAsync(int userId, int page, int pageSize)
    {
        var completedItems = await _completedRepository.GetUserCompletedItemsAsync(userId, page, pageSize);
        return completedItems.Select(MapToDto);
    }

    public async Task<CompletedResult?> UpdateCompletedAsync(int completedId, UpdateCompletedDto updateCompletedDto)
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

    private static CompletedResult MapToDto(Completed completed) =>
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
