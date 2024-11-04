namespace CitMovie.Models.DataTransferObjects;

public class FollowResult
{
    public int FollowingId { get; set; }
    public int PersonId { get; set; }
    public DateTime FollowedSince { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
};

public class FollowCreateRequest
{
    public int UserId { get; set; }
    public int PersonId { get; set; }
}