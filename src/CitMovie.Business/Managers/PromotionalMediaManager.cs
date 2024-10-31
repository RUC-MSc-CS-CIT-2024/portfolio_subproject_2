using System.Security.AccessControl;

namespace CitMovie.Business;

public class PromotionalMediaManager : IPromotionalMediaManager
{
    private readonly IPromotionalMediaRepository _repository;
    private readonly IMapper _mapper;
    
    public PromotionalMediaManager(IPromotionalMediaRepository promotionalMediaRepository, IMapper mapper)
    {
        _repository = promotionalMediaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PromotionalMediaResult>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize)
    {
        var promotionalMedia = await _repository
            .GetPromotionalMediaOfMediaAsync(mediaId, page, pageSize);
        return _mapper.Map<IEnumerable<PromotionalMediaResult>>(promotionalMedia);
    }

    public async Task<IEnumerable<PromotionalMediaResult>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize)
    {
        var promotionalMedia = await _repository
            .GetPromotionalMediaOfReleaseAsync(mediaId, releaseId, page, pageSize);

        return _mapper.Map<IEnumerable<PromotionalMediaResult>>(promotionalMedia);
    }

    public async Task<PromotionalMediaResult> GetPromotionalMediaByIdAsync(int id, int? mediaId, int? releaseId)
    {
        var promotionalMedia = await _repository.GetPromotionalMediaByIdAsync(id,mediaId,releaseId);

        return _mapper.Map<PromotionalMediaResult>(promotionalMedia);
    }

    public async Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseId, int id)
    {
        return await _repository.DeletePromotionalMediaAsync(mediaId, releaseId, id);
    }

    public async Task<PromotionalMediaResult> CreatePromotionalMediaAsync(int mediaId, int releaseId, PromotionalMediaCreateRequest model)
    {
        
        var createModel = _mapper.Map<PromotionalMedia>(model);
        createModel.ReleaseId = releaseId;
        
        var response = await _repository.CreatePromotionalMediaAsync(mediaId, releaseId, createModel);

        return _mapper.Map<PromotionalMediaResult>(response);

    }


    public async Task<int> GetPromotionalMediaCountAsync(int id, string parameter)
    {
        return await _repository.GetPromotionalMediaCountAsync(id, parameter);
    }
}