namespace CitMovie.Business;

public interface IMediaManager {
    IEnumerable<Media> GetAllMedia(PageQueryParameter page);
    IEnumerable<MediaBasicResult> Search(MediaQueryParameter queryParameter, int? userId);
}