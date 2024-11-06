namespace CitMovie.Models.DomainObjects;

[Table("person")]
public class Person
{
    [Key, Column("person_id")]
    public int PersonId { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }

    [Column("death_date")]
    public DateTime? DeathDate { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("score")]
    public decimal? Score { get; set; }

    [Column("imdb_id")]
    public string? ImdbId { get; set; }

    [Column("name_rating")]
    public decimal? NameRating { get; set; }

    public List<CrewMember> CrewMembers { get; } = [];
    public List<CastMember> CastMembers { get; } = [];

}