namespace CitMovie.Models.DomainObjects;

[Table("related_media")]
public class RelatedMedia
{
    [Key, Column("primary_id")]
    public int PrimaryId { get; set; }
    [Column("related_id")]
    public int RelatedId { get; set; }
    [Column("type")]
    public string Type { get; set; }
    
    [ForeignKey(nameof(PrimaryId))]
    public Media MediaPrimary { get; set; }
    [ForeignKey(nameof(RelatedId))]
    public Media MediaRelated { get; set; }

}
