namespace CitMovie.Models.DataTransferObjects;

public record FollowResult(int FollowingId, int PersonId, DateTime FollowedSince);
public record FollowCreateRequest(int UserId, int PersonId);