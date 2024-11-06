namespace CitMovie.Models.DomainObjects;

[Table("job_category")]
public class JobCategory
{
    [Key, Column("job_category_id")]
    public int JobCategoryId { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    
    public List<CrewMember> CrewMembers { get; } = [];
}
