using MuscleMemoryApp.MVVM.Models;
using System.Text;
using System.Text.Json;

namespace MuscleMemoryApp.MVVM.ViewModels;

public class EditUserInfoViewModel
{
    public User user { get; set; }
    HttpClient client;
    public string token { get; set; }
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";

    public EditUserInfoViewModel(string _token)
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        token = _token;
        user = new User();

    }
    public EditUserInfoViewModel()
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        token = string.Empty;
        user = new User();

    }

    public async Task EditUserInfo(string token)
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        string json = JsonSerializer.Serialize(user, serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{baseUrl}/api/identity", content);
    }
}
