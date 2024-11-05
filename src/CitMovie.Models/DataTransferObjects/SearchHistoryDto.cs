namespace CitMovie.Models.DataTransferObjects;


public class SearchHistoryResult : BaseResult
{
    public int SearchHistoryId { get; set; }
    public int UserId { get; set; }
    public string SearchText { get; set; }
    public string Type { get; set; }
}
