namespace CitMovie.Models.DataTransferObjects;

public class BookmarkDto : BaseResult
{
    public int BookmarkId { get; set; }
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public string? Note { get; set; }
}

    public class CreateBookmarkDto : BaseResult
{
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public string? MediaTitle { get; set; }
    public string? Note { get; set; }
}
