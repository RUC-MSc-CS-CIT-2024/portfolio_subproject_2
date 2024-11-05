namespace CitMovie.Models.DataTransferObjects;

public class CompletedDto : BaseResult
{
    public int CompletedId { get; set; }
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}

public class CreateCompletedDto
{
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public DateTime? CompletedDate { get; set; } = DateTime.Now;
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}
