namespace CitMovie.Models.DomainObjects;

[Table("cast_member")]
public class CastMember : CrewBase
{
    [Key, Column("cast_member_id")]
    public override int Id { get; set; }
    [Column("character")]
    public required string Character { get; set; }
}
