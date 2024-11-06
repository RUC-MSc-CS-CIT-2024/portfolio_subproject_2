namespace CitMovie.Models.DomainObjects;

[Table("following")]
public class Follow
{
    [Key]
    [Column("following_id")]
    public int FollowingId { get; set; }

    [Required, Column("user_id")]
    public required int UserId { get; set; }

    [Column("person_id")]
    public required int PersonId { get; set; }

    [Column("followed_since")]
    public DateTime FollowedSince { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
