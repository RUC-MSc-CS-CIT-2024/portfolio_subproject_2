using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.FollowRepository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly FrameworkContext _context;

        public FollowRepository(FrameworkContext context)
        {
            _context = context;
        }

        public async Task<IList<Follow>> GetFollowings(int userId, int page, int pageSize)
        {
            return await _context.Follows
                .Where(f => f.UserId == userId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalFollowingsCount(int userId)
        {
            return await _context.Follows.CountAsync(f => f.UserId == userId);
        }

        public async Task<Follow> CreateFollow(int userId, int personId)
        {
            var follow = new Follow
            {
                UserId = userId,
                PersonId = personId,
                FollowedSince = DateTime.UtcNow
            };

            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();

            return follow;
        }

        public async Task<bool> RemoveFollowing(int userId, int followingId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FollowingId == followingId);

            if (follow == null)
            {
                return false;
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}