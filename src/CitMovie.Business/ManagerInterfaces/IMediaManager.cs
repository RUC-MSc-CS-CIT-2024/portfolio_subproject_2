namespace CitMovie.Business;

public interface IMediaManager {
    IEnumerable<Media> GetAllMedia(PageQueryParameter page);
    IEnumerable<MediaBasicResult> SearchExactMatch(string[] keywords, PageQueryParameter page);
}