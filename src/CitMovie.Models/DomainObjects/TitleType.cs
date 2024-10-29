namespace CitMovie.Models.DomainObjects;

[Table("title_type")]
public class TitleType
{
    [Key, Column("title_type_id")]
    public int TitleTypeId { get; set; }
    [Column("name")]
    public string Name { get; set; }
    
    public List<Title> Titles { get; } = [];
}
