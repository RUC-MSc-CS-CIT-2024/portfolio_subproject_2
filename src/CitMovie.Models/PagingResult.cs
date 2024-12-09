    namespace CitMovie.Models;
    public class PagingResult<T>
    {
        public required int NumberOfItems { get; set; }
        public required int NumberPages { get; set; }
        public string? CurPage { get; set; }
        public string? NextPage { get; set; }
        public string? PrevPage { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
    }