namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaResult : BaseResult
{
    public required int PromotionalMediaId { get; set; }
    public required int ReleaseId { get; set; }
    public required string Type { get; set; }
    public required string Uri { get; set; }
}

public class PromotionalMediaCreateRequest
{
    public required string Type { get; set; }
    public required string Uri { get; set; }

}
