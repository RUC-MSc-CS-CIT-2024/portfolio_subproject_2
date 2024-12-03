namespace CitMovie.Models.DataTransferObjects;

public class BookmarkResult : BaseResult
{
    public int BookmarkId { get; set; }
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public string? Note { get; set; }
    public BookmarkMediaResult? Media { get; set; }

    public class BookmarkMediaResult {
        public required int Id { get; set; }
        public required string Type { get; set; }
        public required string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? AgeRating { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string? PosterUri { get; set; }
        public string? ImdbId { get; set; }
    }
}

public class BookmarkCreateRequest
{
    public required int MediaId { get; set; }
    public string? Note { get; set; }
}

public class BookmarkMoveRequest 
{
    public DateTime? CompletedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.CopenhagenTimeZone);
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}
