namespace CitMovie.Models.DomainObjects;

[Table("production_company")]
public class ProductionCompany
{
    [Key, Column("production_company_id")]
    public int ProductionCompanyId { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }

    public MediaProductionCompany? MediaProductionCompany { get; set; }
}
