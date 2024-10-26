using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitMovie.Models.DomainObjects;


[Table("user")]
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required, MaxLength(50), Column("username")]
    public string Username { get; set; }

    [Required, MaxLength(255), Column("password")]
    public string Password { get; set; }

    [Required, MaxLength(100), Column("email")]
    public string Email { get; set; }

    public ICollection<SearchHistory> SearchHistories { get; set; }
    public ICollection<Bookmark> Bookmarks { get; set; }
    public ICollection<Completed> Completeds { get; set; }
    public ICollection<UserScore> UserScores { get; set; }
    public ICollection<Follow> Followings { get; set; }
}
