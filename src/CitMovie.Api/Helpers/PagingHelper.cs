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

    public string? GetLink(string linkName, object routeValues)
    {
        var uri = _linkGenerator.GetUriByName(
            _httpContextAccessor.HttpContext,
            linkName,
            routeValues
        );
        return uri;
    }

    public object CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T?> items, object additionalRouteValues = null)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var routeValues = new Dictionary<string, object>
        {
            { "page", page },
            { "pageSize", pageSize }
        };

        if (additionalRouteValues != null)
        {
            routeValues = MergeObjects(routeValues, additionalRouteValues);
        }

        var curPage = GetLink(linkName, routeValues) ?? string.Empty;

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, MergeObjects(routeValues, new { page = page + 1 })) ?? string.Empty
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, MergeObjects(routeValues, new { page = page - 1 })) ?? string.Empty
            : null;

        var result = new
        {
            CurPage = curPage,
            NextPage = nextPage,
            PrevPage = prevPage,
            NumberOfItems = total,
            NumberPages = numberOfPages,
            Items = items
        };
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
}