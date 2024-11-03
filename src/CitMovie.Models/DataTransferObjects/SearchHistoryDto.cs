namespace CitMovie.Models.DataTransferObjects;


public record SearchHistoryResult
{
    public int SearchHistoryId { get; set; }
    public int UserId { get; set; }
    public string SearchText { get; set; }
    public string Type { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}

