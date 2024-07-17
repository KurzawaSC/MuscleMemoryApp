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
        await Navigation.PushAsync(new AddExerciseView(token));
    }
}