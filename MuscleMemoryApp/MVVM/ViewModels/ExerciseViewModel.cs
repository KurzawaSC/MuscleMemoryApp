using MuscleMemoryApp.MVVM.Models;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MuscleMemoryApp.MVVM.ViewModels;
[AddINotifyPropertyChangedInterface]
public class ExerciseViewModel
{
    HttpClient client;
    JsonSerializerOptions serializerOptions;
    public ObservableCollection<Exercise> exercises { get; set; }
    App app;

    public ExerciseViewModel()
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        exercises = new ObservableCollection<Exercise>();
        app = (App)Application.Current!;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await GetAllExercise();
    }

    public async Task GetAllExercise()
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);
        var response = await client.GetAsync($"{app.baseUrl}/api/exercises?searchPhrase=");

        if (response.IsSuccessStatusCode)
        {
            using (var responseStream =
                await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Exercise>>(responseStream, serializerOptions);
                exercises = data!;
            }
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            app.BearerToken = null;
            await app.MainPage!.DisplayAlert("Something went wrong", "Unauthorized", "OK");
            app.Quit();
        }
        else
        {
            await app.MainPage!.DisplayAlert("Something went wrong", "", "OK");
        }
    }

    public async Task DeleteExercise(string id)
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);
        var response = await client.DeleteAsync($"{app.baseUrl}/api/exercises/{id}");
        if (response.IsSuccessStatusCode)
        {
            await GetAllExercise();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            app.BearerToken = null;
            await app.MainPage!.DisplayAlert("Something went wrong", "Unauthorized", "OK");
            app.Quit();
        }
        else
        {
            await app.MainPage!.DisplayAlert("Something went wrong", "", "OK");
        }
    }
}
