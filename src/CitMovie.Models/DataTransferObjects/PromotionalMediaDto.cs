namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaDto
{
    public int PromotionalMediaId { get; set; }
    public int ReleaseId { get; set; } // not sure we need it
    public string Type { get; set; }
    public string Uri { get; set; }
    
}

public class CreatePromotionalMediaDto
{
    public int ReleaseId { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }

}