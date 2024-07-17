using MuscleMemoryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MuscleMemoryApp.MVVM.ViewModels;

public class AddExerciseViewModel
{
    HttpClient client;
    string token { get; set; }
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";

    public AddExerciseViewModel(string _token)
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        token = _token;
    }

    public async Task AddExercise()
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



        //var response = await client.PostAsync($"{baseUrl}/api/exercises");
    }
}

