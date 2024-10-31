namespace CitMovie.Models.DataTransferObjects;

public record UserScoreResult(int UserId, int MediaId, int Score, string? ReviewText);
public record USerscoreRequest(string ImdbId, int Score, string ReviewText);