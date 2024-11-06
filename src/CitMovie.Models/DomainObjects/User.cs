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

    public List<SearchHistory> SearchHistories { get; } = [];
    public List<Bookmark> Bookmarks { get; } = [];
    public List<Completed> Completeds { get; } = [];
    public List<UserScore> UserScores { get; } = [];
    public List<Follow> Followings { get; } = [];
}
