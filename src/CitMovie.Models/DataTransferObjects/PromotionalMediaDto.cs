namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaDisplayInfoDto
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}

public class PromotionalMediaResultDto
{
    public int PromotionalMediaId { get; set; }
    public int MediaId { get; set; }
    public int ReleaseId { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}

public class CreatePromotionalMediaDto
{
    public string Type { get; set; }
    public string Uri { get; set; }

}