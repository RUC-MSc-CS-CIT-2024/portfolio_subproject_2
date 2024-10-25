namespace CitMovie.Models.DomainObjects;

[Table("promotional_media")]
public class PromotionalMedia
{
    [Key, Column("promotional_media_id")]
    public int PromotionalMediaId { get; set; }
    [Column("release_id")]
    public int ReleaseId { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("uri")]
    public string Uri { get; set; }
    
    [ForeignKey(nameof(ReleaseId))]
    public Release Release { get; set; }
}
