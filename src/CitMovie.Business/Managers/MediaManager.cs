namespace CitMovie.Business;

public class MediaManager : IMediaManager {
    private readonly IMediaRepository _mediaRepository;

    public MediaManager(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public IEnumerable<Media> GetAllMedia()
    {
        return _mediaRepository.GetAllMedia();
    }
}
