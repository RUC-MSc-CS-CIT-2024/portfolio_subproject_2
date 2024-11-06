using System.ComponentModel;

namespace CitMovie.Models;

public record PageQueryParameter {
    [FromQuery(Name = "page"), DefaultValue(1)]
    public int Number { get; set; } = 1;

    [FromQuery(Name = "count"), DefaultValue(10)]
    public int Count { get; set; } = 10;
}
