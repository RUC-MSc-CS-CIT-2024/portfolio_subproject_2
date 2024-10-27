using CitMovie.Models.DomainObjects;
using CitMovie.Models.DTOs;
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

        public async Task<IList<FollowDto>> GetFollowingsAsync(int userId, int page, int pageSize)
        {
            return await _context.Follows
                .Where(f => f.UserId == userId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(f => new FollowDto
                {
                    FollowingId = f.FollowingId,
                    PersonId = f.PersonId,
                    FollowedSince = f.FollowedSince
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalFollowingsCountAsync(int userId)
        {
            return await _context.Follows.CountAsync(f => f.UserId == userId);
        }

        public async Task CreateFollowAsync(Follow follow)
        {
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveFollowingAsync(int userId, int followingId)
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