using MuscleMemoryApp.MVVM.Models;
using MuscleMemoryApp.MVVM.ViewModels;
using PropertyChanged;

namespace MuscleMemoryApp.MVVM.Views;
[AddINotifyPropertyChangedInterface]
public partial class ExerciseDetailsView : ContentPage
{
    ExerciseDetailsViewModel _viewModel;
    public string message { get; set; }
    public string id;
	public ExerciseDetailsView(string _message, string _token, string _id = "None",
        ExerciseDetails _updatedExercise = default!)
	{
		InitializeComponent();
        _viewModel = new ExerciseDetailsViewModel(_token, _message, _updatedExercise);
        BindingContext = _viewModel;
        id = _id;
        message = _message;
    }

    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        if(message == "New Exercise")
        {
            id = await _viewModel.AddExercise();
        }
        else
        {
            await _viewModel.UpdateExercise(id);
        }
        
        if(_viewModel.hasImage == true && id != "None")
        {
            await _viewModel.AddImage(id);
        }
        await Navigation.PopAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void AddImage_Clicked(object sender, EventArgs e)
    {
        await _viewModel.SelectImage();
    }
}