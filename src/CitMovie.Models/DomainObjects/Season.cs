namespace CitMovie.Models.DomainObjects;

[Table("season")]
public class Season : Media
{
    [Column("status")]
    public required string Status { get; set; }

    [Column("season_number")]
    public int? SeasonNumber { get; set; }

    [Column("end_date")]
    public DateTime? EndDate { get; set; }

    [Column("series_id")]
    public required int SeriesId { get; set; }

    public required Media Series { get; set; }
}
