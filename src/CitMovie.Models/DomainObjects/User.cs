namespace CitMovie.Models.DomainObjects;


[Table("user")]
public class User
{
    [Key]
    [Column("user_id")]
    public int Id { get; set; }

    [MaxLength(50), Column("username")]
    public required string Username { get; set; }

    [MaxLength(255), Column("password")]
    public required string Password { get; set; }

    [MaxLength(100), Column("email")]
    public required string Email { get; set; }

    public ICollection<SearchHistory> SearchHistories { get; } = new List<SearchHistory>();
    public ICollection<Bookmark> Bookmarks { get; } = new List<Bookmark>();
    public ICollection<Completed> Completeds { get; } = new List<Completed>();
    public ICollection<UserScore> UserScores { get; } = new List<UserScore>();
    public ICollection<Follow> Followings { get; } = new List<Follow>();
}
