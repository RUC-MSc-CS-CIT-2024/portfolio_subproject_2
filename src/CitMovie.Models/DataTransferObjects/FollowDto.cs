namespace CitMovie.Models.DataTransferObjects;

public class FollowResult : BaseResult
{
    public required int FollowingId { get; set; }
    public required int PersonId { get; set; }
    public required DateTime FollowedSince { get; set; }
    public FollowPersonResult? Person { get; set; }

    public class FollowPersonResult {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? ImdbId { get; set; }
    }
};

public class FollowCreateRequest
{
    public required int UserId { get; set; }
    public required int PersonId { get; set; }
}
