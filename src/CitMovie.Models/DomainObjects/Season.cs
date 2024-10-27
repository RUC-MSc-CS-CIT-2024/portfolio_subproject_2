namespace CitMovie.Models.DomainObjects;

[Table("Season")]
public class Season
{
    [Key, Column("media_id")]
    public int MediaId { get; set; }
    [Column("status")]
    public string Status { get; set; }
    [Column("season_number")]
    public int SeasonNumber { get; set; }
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    [Column("series_id")]
    public int SeriesId { get; set; }

    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
    [ForeignKey(nameof(SeriesId))]
    public Media Series { get; set; }
    
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
}
