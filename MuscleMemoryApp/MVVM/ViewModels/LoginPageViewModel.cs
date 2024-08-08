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
    App app;

    HttpClient client;
    JsonSerializerOptions _serializerOptions;
    public LoginPageViewModel()
    {
        client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        app = (App)Application.Current!;
    }

    public async Task<bool> LogIn()
    {
        var url = $"{app.baseUrl}/api/identity/login";
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

                var loginResponse = new LoginResponse();
                loginResponse = data!;

                app.BearerToken = loginResponse.accessToken;
                await SecureStorage.Default.SetAsync("bearer_token", loginResponse.accessToken);
                return true;
            }
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            app.BearerToken = null;
            await app.MainPage!.DisplayAlert("Wrong data", "", "OK");
            return false;
        }
        else
        {
            await app.MainPage!.DisplayAlert("Something went wrong", "", "OK");
            return false;
        }
    }
}
