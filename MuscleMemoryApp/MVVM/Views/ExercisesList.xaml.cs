using Microsoft.Maui.Controls;
using MuscleMemoryApp.MVVM.Models;
using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class ExercisesList : ContentPage
{
    string token;
    ExerciseViewModel _viewModel;
	public ExercisesList(string _token)
	{
		InitializeComponent();
		_viewModel = new ExerciseViewModel(_token);
        BindingContext = _viewModel;
        token = _token;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetAllExercise();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExerciseDetailsView("New Exercise", token));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await _viewModel.DeleteExercise(button.CommandParameter.ToString()!);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var exercise = _viewModel.exercises.First(e => e.Id.ToString() == button.CommandParameter.ToString()!);
        var updatedExercise = new ExerciseDetails()
        {
            Name = exercise.Name!,
            Weight = double.Parse(new string(exercise.Record!.TakeWhile(c => c != 'x').ToArray())),
            Reps = int.Parse(new string(exercise.Record!.SkipWhile(x => x != 'x').Skip(1).ToArray()))
        };

        await Navigation.PushAsync(new ExerciseDetailsView("Edit exercise", token,
            button.CommandParameter.ToString()!, updatedExercise));
    }
}