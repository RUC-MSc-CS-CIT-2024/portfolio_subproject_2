namespace CitMovie.Models.DTOs
{
   public class CreateCompletedDto
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int Rewatchability { get; set; }
        public string? Note { get; set; }
    }
}