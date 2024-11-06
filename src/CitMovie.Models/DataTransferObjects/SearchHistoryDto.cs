namespace CitMovie.Models.DataTransferObjects;


public class SearchHistoryResult : BaseResult
{
    public int SearchHistoryId { get; set; }
    public required int UserId { get; set; }
    public required string SearchText { get; set; }
    public required string Type { get; set; }
}
