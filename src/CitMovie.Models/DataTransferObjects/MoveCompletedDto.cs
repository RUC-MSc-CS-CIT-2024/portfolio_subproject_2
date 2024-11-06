namespace CitMovie.Models;
public class MoveCompletedDto
{
    public int UserId { get; set; }
    public int MediaId { get; set; }
    public int Rewatchability { get; set; }
    public string? Note { get; set; }
}
