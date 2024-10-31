using System.Security.AccessControl;

namespace CitMovie.Business;

public class PromotionalMediaManager : IPromotionalMediaManager
{
    private readonly IPromotionalMediaRepository _repository;
    
    public PromotionalMediaManager(IPromotionalMediaRepository promotionalMediaRepository)
    {
        _repository = promotionalMediaRepository;
    }

    public async Task<IEnumerable<PromotionalMediaResultDto>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize)
    {
        var promotionalMedia = await _repository
            .GetPromotionalMediaOfMediaAsync(mediaId, page, pageSize);
        
        return promotionalMedia.Select(p => new PromotionalMediaResultDto
        {
            PromotionalMediaId = p.PromotionalMediaId,
            MediaId = p.Release.MediaId,
            ReleaseId = p.ReleaseId,
            Type = p.Type,
            Uri = p.Uri
        });
    }

    public async Task<IEnumerable<PromotionalMediaResultDto>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize)
    {
        var promotionalMedia = await _repository
            .GetPromotionalMediaOfReleaseAsync(mediaId, releaseId, page, pageSize);

        return promotionalMedia.Select(p => new PromotionalMediaResultDto
        {
            PromotionalMediaId = p.PromotionalMediaId,
            MediaId = p.Release.MediaId,
            ReleaseId = p.ReleaseId,
            Type = p.Type,
            Uri = p.Uri
        });
    }

    public async Task<PromotionalMediaResultDto> GetPromotionalMediaByIdAsync(int id, int? mediaId, int? releaseId)
    {
        var promotionalMedia = await _repository.GetPromotionalMediaByIdAsync(id,mediaId,releaseId);

        return new PromotionalMediaResultDto
        {
            PromotionalMediaId = promotionalMedia.PromotionalMediaId,
            MediaId = promotionalMedia.Release.MediaId,
            ReleaseId = promotionalMedia.ReleaseId,
            Type = promotionalMedia.Type,
            Uri = promotionalMedia.Uri
        };
    }

    public async Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseId, int id)
    {
        return await _repository.DeletePromotionalMediaAsync(mediaId, releaseId, id);
    }

    public async Task<PromotionalMediaResultDto> CreatePromotionalMediaAsync(int mediaId, int releaseId, CreatePromotionalMediaDto model)
    {
        var createModel = new PromotionalMedia 
        {
            ReleaseId = releaseId,
            Type = model.Type,
            Uri = model.Uri,
        };
        
        var response = await _repository.CreatePromotionalMediaAsync(mediaId, releaseId, createModel);

        var result = new PromotionalMediaResultDto
        {
            PromotionalMediaId = response.PromotionalMediaId,
            MediaId = mediaId,
            ReleaseId = response.ReleaseId,
            Type = response.Type,
            Uri = response.Uri
        };
        return (result);
    }


    public async Task<int> GetPromotionalMediaCountAsync(int id, string parameter)
    {
        return await _repository.GetPromotionalMediaCountAsync(id, parameter);
    }
}