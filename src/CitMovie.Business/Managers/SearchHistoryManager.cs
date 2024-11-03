namespace CitMovie.Business;

public class SearchHistoryManager : ISearchHistoryManager
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IMapper _mapper;

    public SearchHistoryManager(ISearchHistoryRepository searchHistoryRepository, IMapper mapper)
    {
        _searchHistoryRepository = searchHistoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SearchHistoryResult>> GetUserSearchHistoriesAsync(int userId, int page, int pageSize)
    {
        var searchHistories = await _searchHistoryRepository.GetUserSearchHistoriesAsync(userId, page, pageSize);
        return _mapper.Map<IEnumerable<SearchHistoryResult>>(searchHistories);
    }

    public async Task<int> GetUsersTotalSearchHistoriesCountAsync(int userId)
    {
        return await _searchHistoryRepository.GetUsersTotalSearchHistoriesCountAsync(userId);
    }

    public async Task<bool> DeleteUsersSearchHistoriesAsync(int userId, int searchHistoryId)
    {
        return await _searchHistoryRepository.DeleteUsersSearchHistoriesAsync(userId, searchHistoryId);
    }

}
