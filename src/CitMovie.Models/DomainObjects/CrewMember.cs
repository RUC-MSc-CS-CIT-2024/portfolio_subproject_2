namespace CitMovie.Models.DomainObjects;

public abstract class CrewBase
{
    public abstract int Id { get; set; }
    
    [Column("role")]
    public required string Role { get; set; }
    [Column("person_id")]
    public required int PersonId { get; set; }
    [Column("media_id")]
    public required int MediaId { get; set; }
    
    public Media? Media { get; set; }
    public Person? Person { get; set; }
}

[Table("crew_member")]
public class CrewMember : CrewBase
{
    [Key, Column("crew_member_id")]
    public override int Id { get; set; }

    [Column("job_category_id")]
    public required int JobCategoryId { get; set; }
    
    public JobCategory? JobCategory { get; set; }
}
