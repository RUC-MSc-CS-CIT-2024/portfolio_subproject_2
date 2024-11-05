namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaMinimalInfoResult : BaseResult
{
    public string PromotionalMediaId { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}

public class PromotionalMediaResult
{
    public int PromotionalMediaId { get; set; }
    public int MediaId { get; set; }
    public int ReleaseId { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}

public class PromotionalMediaCreateRequest
{
    public string Type { get; set; }
    public string Uri { get; set; }

}
