using System.Text.Json.Serialization;

namespace CitMovie.Models.DomainObjects;


[Table("following")]
public class Follow
{
    [Key]
    [Column("following_id")]
    public int FollowingId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("followed_since")]
    public DateTime FollowedSince { get; set; } = DateTime.Now;
}
