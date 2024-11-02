namespace CitMovie.Business;

public interface IMediaManager {
    IEnumerable<Media> GetAllMedia();
}