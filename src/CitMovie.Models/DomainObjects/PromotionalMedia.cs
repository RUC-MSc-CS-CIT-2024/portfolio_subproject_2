namespace CitMovie.Models.DomainObjects;

[Table("promotional_media")]
public class PromotionalMedia
{
    [Key, Column("promotional_media_id")]
    public int PromotionalMediaId { get; set; }
    [Column("release_id")]
    public required int ReleaseId { get; set; }
    [Column("type")]
    public required string Type { get; set; }
    [Column("uri")]
    public required string Uri { get; set; }
    
    public Release? Release { get; set; }
}
