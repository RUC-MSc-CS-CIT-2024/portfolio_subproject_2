using System.Security.AccessControl;

namespace CitMovie.Business;

public class PromotionalMediaManager : IPromotionalMediaManager
{
    private readonly IPromotionalMediaRepository _repository;
    
    public PromotionalMediaManager(IPromotionalMediaRepository promotionalMediaRepository)
    {
        _repository = promotionalMediaRepository;
    }

    public async Task<IEnumerable<PromotionalMediaDto>> GetPromotionalMediaAsync(int page, int pageSize)
    {
        var promotionalMedia = await _repository
            .GetPromotionalMediaAsync(page, pageSize);

        return promotionalMedia.Select(p => new PromotionalMediaDto
        {
            PromotionalMediaId = p.PromotionalMediaId,
            ReleaseId = p.ReleaseId,
            Type = p.Type,
            Uri = p.Uri
        });
    }

    public async Task<PromotionalMediaDto> GetPromotionalMediaByIdAsync(int id)
    {
        var promotionalMedia = await _repository.GetPromotionalMediaByIdAsync(id);

        return new PromotionalMediaDto
        {
            PromotionalMediaId = promotionalMedia.PromotionalMediaId,
            ReleaseId = promotionalMedia.ReleaseId,
            Type = promotionalMedia.Type,
            Uri = promotionalMedia.Uri
        };
    }

    public async Task<bool> DeletePromotionalMediaAsync(int id)
    {
        return await _repository.DeletePromotionalMediaAsync(id);
    }

    public async Task<PromotionalMediaDto> CreatePromotionalMediaAsync(CreatePromotionalMediaDto model)
    {
        var createModel = new PromotionalMedia
        {
            ReleaseId = model.ReleaseId,
            Type = model.Type,
            Uri = model.Uri,
        };
        
        var response = await _repository.CreatePromotionalMediaAsync(createModel);

        var result = new PromotionalMediaDto
        {
            PromotionalMediaId = response.PromotionalMediaId,
            ReleaseId = response.ReleaseId,
            Type = response.Type,
            Uri = response.Uri
        };
        return (result);
    }


    public async Task<int> GetPromotionalMediaCountAsync()
    {
        return await _repository.GetPromotionalMediaCountAsync();
    }
}