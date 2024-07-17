using MuscleMemoryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace MuscleMemoryApp.MVVM.ViewModels;

public class AddExerciseViewModel
{
    HttpClient client;
    string token { get; set; }
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";
    public newExercise _newExercise { get; set; }

    public AddExerciseViewModel(string _token)
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        token = _token;
        _newExercise = new newExercise();
        
    }

    public async Task AddExercise()
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        string json = JsonSerializer.Serialize(_newExercise, serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await client.PostAsync($"{baseUrl}/api/exercises", content);
    }
}

