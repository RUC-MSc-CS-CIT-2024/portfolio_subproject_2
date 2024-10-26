namespace CitMovie.Models.DomainObjects;

[Table("cast_member")]
public class CastMember
{
    [Key, Column("cast_member_id")]
    public int CastMemberId { get; set; }
    [Column("role")]
    public string? Role { get; set; }
    [Column("character")]
    public string Character { get; set; }
    [Column("person_id")]
    public int PersonId { get; set; }
    [Column("media_id")]
    public int MediaId { get; set; }
    
    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; }
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
}
