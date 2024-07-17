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
    string token { get; set; }
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";
    public ObservableCollection<Exercise> exercises { get; set; }

    public ExerciseViewModel(string _token)
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        exercises = new ObservableCollection<Exercise>();
        token = _token;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        exercises = await GetAllExercise();
    }

    public ICommand GetAllExerciseCommand =>
        new Command(async () =>
        {
            exercises = await GetAllExercise();
        });

    public async Task<ObservableCollection<Exercise>> GetAllExercise()
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"{baseUrl}/api/exercises?searchPhrase=");

        if (response.IsSuccessStatusCode)
        {
            using (var responseStream =
                await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Exercise>>(responseStream, serializerOptions);
                return data!;
            }
        }
        else
        {
            return exercises;
        }
    }
}
