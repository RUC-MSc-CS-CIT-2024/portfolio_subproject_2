namespace CitMovie.Models.DomainObjects;

[Table("title_attribute")]
public class TitleAttribute
{
    [Key, Column("title_attribute_id")]
    public int TitleAttributeId { get; set; }
    [Column("name")]
    public int Name { get; set; }

    
    public List<Title> Titles { get; } = [];
}
