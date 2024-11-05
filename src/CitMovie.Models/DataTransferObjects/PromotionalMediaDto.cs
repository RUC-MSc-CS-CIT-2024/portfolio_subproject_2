namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaMinimalInfoResult
{
    public string PromotionalMediaId { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
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