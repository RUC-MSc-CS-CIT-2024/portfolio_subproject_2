namespace CitMovie.Business;

public class CompletedManager : ICompletedManager
{
    private readonly ICompletedRepository _completedRepository;
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IMediaRepository _mediaRepository;

    private readonly IMapper _mapper;

    public CompletedManager(
        ICompletedRepository completedRepository, 
        IBookmarkRepository bookmarkRepository,
        IMediaRepository mediaRepository,
        IMapper mapper)
    {
        _completedRepository = completedRepository;
        _bookmarkRepository = bookmarkRepository;
        _mediaRepository = mediaRepository;

        _mapper = mapper;
    }

    public async Task<CompletedResult> MoveBookmarkToCompletedAsync(int userId, int bookmarkId, BookmarkMoveRequest createCompletedDto)
    {
        Bookmark b = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
        if (b.UserId != userId)
            throw new InvalidOperationException();

        var completed = await _completedRepository.MoveBookmarkToCompletedAsync(userId, b.MediaId, createCompletedDto.Rewatchability, createCompletedDto.Note);
        return _mapper.Map<CompletedResult>(completed);
    }

    public async Task<CompletedResult?> GetCompletedAsync(int completedId)
    {
        Completed? completed = await _completedRepository.GetCompletedByIdAsync(completedId);
        if (completed == null) 
            return null;

        CompletedResult result = _mapper.Map<CompletedResult>(completed);
        Media? media = _mediaRepository.GetDetailed(completed.MediaId);
        if (media != null) 
            result.Media = _mapper.Map<CompletedResult.CompletedMediaResult>(media);
        return result;
    }

    public async Task<IEnumerable<CompletedResult>> GetUserCompletedItemsAsync(int userId, int page, int pageSize)
    {
        IEnumerable<Completed> completedItems = await _completedRepository.GetUserCompletedItemsAsync(userId, page, pageSize);
        IEnumerable<CompletedResult> result = _mapper.Map<IEnumerable<CompletedResult>>(completedItems);
        foreach (CompletedResult completed in result) {
            Media? media = _mediaRepository.GetDetailed(completed.MediaId);
            if (media != null) 
                completed.Media = _mapper.Map<CompletedResult.CompletedMediaResult>(media);
        }
        return result;
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
        return _mapper.Map<CompletedResult>(updated);
    }

    public async Task<bool> DeleteCompletedAsync(int completedId) =>
        await _completedRepository.DeleteCompletedAsync(completedId);


    public async Task<int> GetTotalUserCompletedCountAsync(int userId)
    {
        return await _completedRepository.GetTotalUserCompletedCountAsync(userId);
    }

    public async Task<CompletedResult> CreateBookmarkAsync(int userId, CompletedCreateRequest createRequest)
    {
        Completed c = _mapper.Map<Completed>(createRequest);
        c.UserId = userId;

        Completed result = await _completedRepository.CreateCompletedAsync(c);
        return _mapper.Map<CompletedResult>(result);
    }
}
