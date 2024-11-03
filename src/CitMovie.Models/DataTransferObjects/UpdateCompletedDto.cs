namespace CitMovie.Models.DataTransferObjects;

public class UpdateCompletedDto
{
    public DateTime? CompletedDate { get; set; }
    public int? Rewatchability { get; set; }
    public string? Note { get; set; }
}
