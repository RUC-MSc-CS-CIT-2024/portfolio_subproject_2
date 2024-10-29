namespace CitMovie.Models.DataTransferObjects;

public class PromotionalMediaDto
{
    public int PromotionalMediaId { get; set; }
    public int ReleaseId { get; set; } // not sure we need it
    public string Type { get; set; }
    public string Uri { get; set; }
    
}