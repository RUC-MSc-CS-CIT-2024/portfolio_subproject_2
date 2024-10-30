using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers
{
    public interface ICompletedManager
    {
        Task<CompletedDto> AddCompletedAsync(CreateCompletedDto completedDto);
        Task<CompletedDto?> GetCompletedAsync(int completedId);
        Task<IEnumerable<CompletedDto>> GetUserCompletedAsync(int userId);
        Task<bool> DeleteCompletedAsync(int completedId);
    }
}
