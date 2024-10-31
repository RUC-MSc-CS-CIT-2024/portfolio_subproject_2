namespace CitMovie.Models.DataTransferObjects;

public record UserScoreResult(int UserId, int mediaId, int score, string ReviewText);