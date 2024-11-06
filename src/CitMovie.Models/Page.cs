namespace CitMovie.Models;

public record PageQueryParameter {
    [FromQuery(Name = "page")]
    public int Number { get; set; } = 1;

    [FromQuery(Name = "count")]
    public int Count { get; set; } = 10;
}
