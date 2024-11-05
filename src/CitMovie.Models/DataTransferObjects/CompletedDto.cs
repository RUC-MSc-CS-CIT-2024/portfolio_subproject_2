namespace CitMovie.Models.DataTransferObjects;

public static class TimeZoneHelper
{
    public static readonly TimeZoneInfo CopenhagenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
}

public class CompletedDto : BaseResult
{
    public int CompletedId { get; set; }
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}

public class CreateCompletedDto : BaseResult
{
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.CopenhagenTimeZone);
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}
