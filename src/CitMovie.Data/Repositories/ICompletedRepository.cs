namespace CitMovie.Data.Repositories
{
    public interface ICompletedRepository
    {
        Task<Completed> AddCompletedAsync(Completed completed);
        Task<Completed?> GetCompletedAsync(int completedId);
        Task<IEnumerable<Completed>> GetUserCompletedAsync(int userId);
        Task<bool> DeleteCompletedAsync(int completedId);
    }
}
