namespace CitMovie.Api;

public class PagingHelper
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PagingHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    private string? GetLink(string linkName, object routeValues)
        => _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext!, linkName, routeValues);

    public PagingResult<T> CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T> items, object? additionalRouteValues = null)
    {
        Dictionary<string, object> routeValues = new() {
            ["page"] = page,
            ["count"] = pageSize
        };

        if (additionalRouteValues != null)
            routeValues = MergeObjects(routeValues, additionalRouteValues);

        PagingResult<T> result = new() {
            CurPage = GetLink(linkName, routeValues),
            NumberPages = (int)Math.Ceiling(total / (double)pageSize),
            NumberOfItems = total,
            Items = items
        };
        
        if (page < result.NumberPages)
            result.NextPage = GetLink(linkName, MergeObjects(routeValues, new { page = page + 1 }));

        if (page > 1)
            result.PrevPage = GetLink(linkName, MergeObjects(routeValues, new { page = page - 1 }));

        return result;
    }

    private Dictionary<string, object> MergeObjects(Dictionary<string, object> existingRouteValues, object additionalRouteValues)
    {
        foreach (var property in additionalRouteValues.GetType().GetProperties())
        {
            var value = property.GetValue(additionalRouteValues);
            if (value != null)
            {
                existingRouteValues[property.Name] = value;
            }
        }
        return existingRouteValues;
    }

    public string? GetResourceLink(string routeName, object routeValues)
        => GetLink(routeName, routeValues);
}