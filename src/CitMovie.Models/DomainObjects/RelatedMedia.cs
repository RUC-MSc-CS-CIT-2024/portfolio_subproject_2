namespace CitMovie.Models.DomainObjects;

[PrimaryKey(nameof(PrimaryId), nameof(RelatedId)), Table("related_media")]
public class RelatedMedia
{
    [Column("primary_id")]
    public required int PrimaryId { get; set; }

    [Column("related_id")]
    public required int RelatedId { get; set; }

    [Column("type", TypeName = "varchar(50)")]
    public required RelatedMediaType Type { get; set; }
    
    public Media? Primary { get; set; }
    public Media? Related { get; set; }

}