namespace CitMovie.Models.DomainObjects;

[Table("crew_member")]
public class CrewMember
{
    [Key, Column("crew_member_id")]
    public int CrewMemberId { get; set; }
    [Column("role")]
    public string? Role { get; set; }
    [Column("person_id")]
    public int PersonId { get; set; }
    [Column("media_id")]
    public int MediaId { get; set; }
    [Column("job_category_id")]
    public int JobCategoryId { get; set; }
    
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; }
    [ForeignKey(nameof(JobCategoryId))]
    public JobCategory JobCategory { get; set; }
}
