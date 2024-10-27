namespace CitMovie.Models.DomainObjects;

[Table("related_media")]
public class RelatedMedia
{
    [Key, Column("related_media_id")]
    public int RelatedMediaId { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
}