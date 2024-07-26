using MuscleMemoryApp.MVVM.Models;
using PropertyChanged;
using System.Text;
using System.Text.Json;

namespace MuscleMemoryApp.MVVM.ViewModels;
public class ExerciseDetailsViewModel
{
    HttpClient client;
    JsonSerializerOptions serializerOptions;
    string baseUrl = "https://localhost:7002";
    public ExerciseDetails _newExercise { get; set; }
    public bool hasImage = false;
    FileResult Image = default!;
    public string message { get; set; }
    App app;

    public ExerciseDetailsViewModel(string _message, ExerciseDetails newExercise = default!)
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        if (newExercise == default)
        {
            _newExercise = new ExerciseDetails();
        }else
        {
            _newExercise = newExercise;
        }
        message = _message;
        app = (App)Application.Current!;
    }

    public async Task<string> AddExercise()
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);

        string json = JsonSerializer.Serialize(_newExercise, serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await client.PostAsync($"{baseUrl}/api/exercises", content);
        if (response.IsSuccessStatusCode)
        {
            var locationHeader = response.Headers.Location;
            if (locationHeader != null)
            {
                var uri = locationHeader.ToString();

                var id = uri.Split('/').Last();
                return id;
            }
        }
        return default!;
    }

    public async Task UpdateExercise(string id)
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);

        string json = JsonSerializer.Serialize(_newExercise, serializerOptions);

        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{baseUrl}/api/exercises/{id}", content);
    }


    public async Task SelectImage()
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select an Image",
                FileTypes = FilePickerFileType.Images
            });
            hasImage = true;
            Image = result!;
        }
        catch (Exception)
        {
            // To be continue
        }
    }


    public async Task AddImage(string exerciseId)
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", app.BearerToken);

        var result = Image;

        var imageInBytes = await TurnImageToBytes(result);

        string json = JsonSerializer.Serialize(_newExercise, serializerOptions);

        var content = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(imageInBytes);
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

        var contentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
        {
            Name = "file",
            FileName = Image.FileName
        };
        imageContent.Headers.ContentDisposition = contentDisposition;

        content.Add(imageContent, "image");

        var response = await client.PostAsync($"{baseUrl}/api/exercises/{exerciseId}/picture", content);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to upload image: {errorMessage}");
        }
    }

    public async Task<byte[]> TurnImageToBytes(FileResult result)
    {
        if (result == null)
            return default!;

        using (var stream = await result.OpenReadAsync())
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

}

