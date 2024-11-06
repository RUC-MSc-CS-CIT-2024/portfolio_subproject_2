namespace CitMovie.Models.DomainObjects;

[Table("media_production_company")]
public class MediaProductionCompany
{
    [Column("media_id")]
    public required int MediaId { get; set; }
    [Column("production_company_id")]
    public required int ProductionCompanyId { get; set; }
    [Column("type")]
    public required string Type { get; set; }
    
    public Media? Media { get; set; }
    public ProductionCompany? ProductionCompany { get; set; }
}
