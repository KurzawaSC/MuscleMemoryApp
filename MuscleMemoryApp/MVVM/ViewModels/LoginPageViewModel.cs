using MuscleMemoryApp.MVVM.Models;
using MuscleMemoryApp.MVVM.Views;
using PropertyChanged;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Input;

namespace MuscleMemoryApp.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class LoginPageViewModel
{
    public string eMail { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;

    HttpClient client;
    JsonSerializerOptions _serializerOptions;
    string baseUrl = "https://localhost:7002";
    public LoginResponse loginResponse;
    public LoginPageViewModel()
    {
        client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        loginResponse = new LoginResponse();
    }

    public async Task LogIn()
    {
        var url = $"{baseUrl}/api/identity/login";
        var loginRequest = new LoginRequest(eMail, password);

        string json = JsonSerializer.Serialize<LoginRequest>(loginRequest, _serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            using (var responseStream =
                              await response.Content.ReadAsStreamAsync())
            {
                var data =
                await JsonSerializer
                .DeserializeAsync<LoginResponse>(responseStream, _serializerOptions);
                loginResponse = data!;
            }
        }
    }
}
