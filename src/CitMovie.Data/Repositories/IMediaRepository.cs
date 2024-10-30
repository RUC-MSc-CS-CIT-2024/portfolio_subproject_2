namespace CitMovie.Data;

public interface IMediaRepository {
    IEnumerable<Media> GetAllMedia();
}