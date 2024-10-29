namespace CitMovie.Models.DataTransferObjects
{
    public class CreateBookmarkDto
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string? Note { get; set; }
    }
}
