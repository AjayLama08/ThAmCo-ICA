using System.Text.Json.Serialization;

public class EventTypeDTO
{
    [JsonPropertyName("id")]
    public string EventTypeId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}
