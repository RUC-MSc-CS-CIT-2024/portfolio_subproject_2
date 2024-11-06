namespace CitMovie.Models.DomainObjects;


[Table("completed")]
public class Completed
{
    [Key]
    [Column("completed_id")]
    public int CompletedId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public required User User { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Column("completed_date")]
    public DateTime? CompletedDate { get; set; }

    [Column("rewatchability")]
    [Range(1, 5)]
    public int Rewatchability { get; set; }

    [Column("note")]
    public string? Note { get; set; }
}

