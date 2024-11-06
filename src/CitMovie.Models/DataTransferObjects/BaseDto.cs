namespace CitMovie.Models.DataTransferObjects;
public class BaseResult {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Link>? Links { get; set; }
}