namespace CitMovie.Models;

public record PageQueryParameter(
    [property: FromQuery(Name = "page")] int Number = 1, 
    [property: FromQuery(Name = "count")] int Count = 10);