using System.Text.Json.Serialization;

namespace MuscleMemoryApp.MVVM.Models;

public class Exercise
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("record")]
    public string? Record { get; set; }
    [JsonPropertyName("pictureUrl")]
    public string? PictureUrl { get; set; }
    public ImageSource? Image => PictureUrl == default ? default : ImageSource.FromUri(new Uri(PictureUrl));
}
