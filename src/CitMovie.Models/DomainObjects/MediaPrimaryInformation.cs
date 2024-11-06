namespace CitMovie.Models.DomainObjects;

[Table("media_primary_information")]
public class MediaPrimaryInformation {
    [Key, Column("media_id")]
    public required int MediaId { get; set; }

    [Column("title_id")]
    public required int TitleId { get; set; }

    [Column("release_id")]
    public int? ReleaseId { get; set; }

    [Column("promotional_media_id")]
    public int? PromotionalMediaId { get; set; }

    public Media? Media { get; set; }
    public Title? Title { get; set; }
    public Release? Release { get; set; }
    public PromotionalMedia? PromotionalMedia { get; set; }
}
