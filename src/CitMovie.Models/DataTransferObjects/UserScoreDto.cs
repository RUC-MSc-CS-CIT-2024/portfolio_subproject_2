namespace CitMovie.Models.DataTransferObjects
{
    public record UserScoreResult
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
}