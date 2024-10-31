namespace CitMovie.Models.DomainObjects;

[Table("media_primary_information")]
public class MediaPrimaryInformation {
    [Key, Column("media_id")]
    public int MediaId { get; set; }

    [Column("title_id")]
    public int TitleId { get; set; }

    [Column("release_id")]
    public int? ReleaseId { get; set; }

    [Column("promotional_media_id")]
    public int? PromotionalMediaId { get; set; }

    public Media Media { get; set; } = null!;
    public Title Title { get; set; } = null!;
    public Release? Release { get; set; }
    public PromotionalMedia? PromotionalMedia { get; set; }
}
