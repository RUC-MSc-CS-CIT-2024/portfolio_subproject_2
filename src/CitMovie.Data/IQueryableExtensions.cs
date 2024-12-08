namespace CitMovie.Data;

public static class IQueryableExtensions {
    public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageNumber, int pageCount)
        => query.Skip((pageNumber - 1) * pageCount).Take(pageCount);
    
    public static IQueryable<Media> Filter(this IQueryable<Media> mediaResults, FilterParameters? query)
    {
        if (query == null)
            return mediaResults;

        if (query.Type is not null)
            mediaResults = mediaResults.Where(x => x.Type == query.Type);

        if (query.Genre is not null)
            foreach (var genre in query.Genre)
            {
                mediaResults = mediaResults.Where(x => x.Genres.Any(g => g.Name == CapitalizeEachWord(genre)));
            }

        if (query.IsoCode is not null)
            foreach (var iso in query.IsoCode)
            {
                mediaResults = mediaResults.Where(x => x.Countries.Any(c => c.IsoCode == iso));
            }
        
        if (query.Year is not null)
            mediaResults = mediaResults.Where(x => x.PrimaryInformation!.Release!.ReleaseDate!.Value.Year == query.Year);

        if (query.Company is not null)
            foreach (var company in query.Company)
            {
                mediaResults = mediaResults.Where(x => x.MediaProductionCompany.Any(c => c.ProductionCompany!.Name == company));
            }
        
        return mediaResults;
    }
    
    private static string CapitalizeEachWord(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;
        
        var words = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
        }

        return string.Join(" ", words);
    }
}