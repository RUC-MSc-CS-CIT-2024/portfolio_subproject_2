namespace CitMovie.Business;

public interface IMediaManager {
    IEnumerable<MediaBasicResult> GetAllMedia(PageQueryParameter page);
    IEnumerable<MediaBasicResult> Search(MediaQueryParameter queryParameter, PageQueryParameter pageQuery, int? userId);
    MediaResult Get(int id);
    IEnumerable<MediaBasicResult> GetSimilar(int id, PageQueryParameter page);
    IEnumerable<MediaBasicResult> GetRelated(int id, PageQueryParameter page);
    Task<IEnumerable<CrewResult>> GetCrewAsync(int mediaId, PageQueryParameter page);
    Task<IEnumerable<CrewResult>> GetCastAsync(int mediaId, PageQueryParameter page);
    Task<int> GetTotalRelatedMediaCountAsync(int id);
    Task<int> GetTotalSimilarMediaCountAsync(int id);
    Task<int> GetTotalCrewCountAsync(int id);
    Task<int> GetTotalCastCountAsync(int id);
    int GetSearchResultsCount(MediaQueryParameter query);
    int GetTotalMediaCount();
}