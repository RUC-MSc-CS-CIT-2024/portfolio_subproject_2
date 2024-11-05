namespace CitMovie.Business;

public interface IMediaManager {
    IEnumerable<MediaBasicResult> GetAllMedia(PageQueryParameter page);
    IEnumerable<MediaBasicResult> Search(MediaQueryParameter queryParameter, int? userId);
    MediaResult? Get(int id);
    IEnumerable<MediaBasicResult> GetSimilar(int id, PageQueryParameter page);
    IEnumerable<MediaBasicResult> GetRelated(int id, PageQueryParameter page);
    Task<int> GetTotalRelatedMediaCountAsync(int id);
    Task<int> GetTotalSimilarMediaCountAsync(int id);
}