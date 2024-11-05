namespace CitMovie.Models.DataTransferObjects;

public class FollowResult : BaseResult
{
    public int FollowingId { get; set; }
    public int PersonId { get; set; }
    public DateTime FollowedSince { get; set; }
};

public class FollowCreateRequest
{
    public int UserId { get; set; }
    public int PersonId { get; set; }
}