namespace CitMovie.Models.DataTransferObjects
{
    public class BookmarkDto
    {
        public int BookmarkId { get; set; }
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string? Note { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
