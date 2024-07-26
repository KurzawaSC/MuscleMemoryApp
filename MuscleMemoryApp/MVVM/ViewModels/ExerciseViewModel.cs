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
    string baseUrl = "https://localhost:7002";
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
        var response = await client.GetAsync($"{baseUrl}/api/exercises?searchPhrase=");

        if (response.IsSuccessStatusCode)
        {
            using (var responseStream =
                await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Exercise>>(responseStream, serializerOptions);
                exercises = data!;
            }
        }
    }

    public async Task DeleteExercise(string id)
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);
        var response = await client.DeleteAsync($"{baseUrl}/api/exercises/{id}");
        await GetAllExercise();
    }
}
