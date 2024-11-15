using System.Text.Json.Serialization;

public class EventTypeDTO
{
    [JsonPropertyName("id")]
    public string EventTypeId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}
