namespace CitMovie.Models.DomainObjects;

[Table("media_production_company")]
public class MediaProductionCompany
{
    [Column("media_id")]
    public int MediaId { get; set; }
    [Column("production_company_id")]
    public int ProductionCompanyId { get; set; }
    [Column("type")]
    public string Type { get; set; }
    
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
    [ForeignKey(nameof(ProductionCompanyId))]
    public ProductionCompany ProductionCompany { get; set; }
}
