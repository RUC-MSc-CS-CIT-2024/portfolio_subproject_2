namespace CitMovie.Models.DTOs;

public class FollowDto
{
    public int FollowingId { get; set; }
    public int PersonId { get; set; }
    public DateTime FollowedSince { get; set; }
}