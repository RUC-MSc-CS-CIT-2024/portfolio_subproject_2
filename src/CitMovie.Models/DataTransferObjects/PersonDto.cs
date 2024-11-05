namespace CitMovie.Models.DataTransferObjects;

public class PersonResult : BaseResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Score { get; set; }
    public decimal? NameRating { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
}

public class CoActorResult : BaseResult
{
    public string Id { get; set; }
    public string ActorName { get; set; }
    public int Frequency { get; set; }
}
