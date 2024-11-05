namespace CitMovie.Models.DataTransferObjects;

public class UserScoreResult : BaseResult
{
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public int Score { get; set; }
    public string? ReviewText { get; set; }
}

public record UserScoreCreateRequest
{
    public string ImdbId { get; set; }
    public int Score { get; set; }
    public string? ReviewText { get; set; }
}
