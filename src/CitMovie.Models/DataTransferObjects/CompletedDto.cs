namespace CitMovie.Models.DataTransferObjects;

public static class TimeZoneHelper
{
    public static readonly TimeZoneInfo CopenhagenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
}

public class CompletedResult : BaseResult
{
    public int CompletedId { get; set; }
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
    public CompletedMediaResult? Media { get; set; }

    public class CompletedMediaResult {
        public required int Id { get; set; }
        public required string Type { get; set; }
        public required string Title { get; set; }
        public string? PosterUri { get; set; }
        public string? ImdbId { get; set; }
    } 
}

public class CompletedCreateRequest
{
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.CopenhagenTimeZone);
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}
