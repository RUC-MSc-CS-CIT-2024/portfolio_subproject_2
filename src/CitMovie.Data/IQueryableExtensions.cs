namespace CitMovie.Data;

public static class IQueryableExtensions {
    public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageNumber, int pageCount)
        => query.Skip((pageNumber - 1) * pageCount).Take(pageCount);
}