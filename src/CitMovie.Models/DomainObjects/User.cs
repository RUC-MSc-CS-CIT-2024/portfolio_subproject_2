namespace CitMovie.Models.DomainObjects;


[Table("user")]
public class User
{
    [Key]
    [Column("user_id")]
    public int Id { get; set; }

    [Column("username")]
    public required string Username { get; set; }

    [Column("hashed_password")]
    public required string HashedPassword { get; set; }
    [Column("salt")]
    public required string Salt { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    public List<SearchHistory> SearchHistories { get; } = [];
    public List<Bookmark> Bookmarks { get; } = [];
    public List<Completed> Completeds { get; } = [];
    public List<UserScore> UserScores { get; } = [];
    public List<Follow> Followings { get; } = [];
}
