using System.Text.Json.Serialization;

namespace Arke.ToDoList.WebSite.Shared;

public class TaskItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
}
