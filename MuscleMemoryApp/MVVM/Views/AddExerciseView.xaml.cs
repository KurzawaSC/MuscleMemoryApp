using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class AddExerciseView : ContentPage
{
    AddExerciseViewModel _viewModel;
	public AddExerciseView(string _token)
	{
		InitializeComponent();
        _viewModel = new AddExerciseViewModel(_token);
        BindingContext = _viewModel;
    }

    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        var id = await _viewModel.AddExercise();
        if(_viewModel.hasImage == true)
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