using MuscleMemoryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace MuscleMemoryApp.MVVM.ViewModels;

public class RegisterViewModel
{
    public List<string> sexes { get; set; }
    public RegisterRequest request { get; set; }
    public EditUserInfoViewModel editUserInfo { get; set; }

    HttpClient client;
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";

    public RegisterViewModel()
    {
        sexes = new List<string>()
        {
            "male", "female"
        };
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        request = new RegisterRequest();
        editUserInfo = new EditUserInfoViewModel();
    }

    public async Task RegisterUser()
    {
        string json = JsonSerializer.Serialize(request, serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await client.PostAsync($"{baseUrl}/api/identity/register", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        //var registerResponse = JsonSerializer.Deserialize<RegisterResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //return registerResponse!.Id;

        //response is empty :(
    }
}
