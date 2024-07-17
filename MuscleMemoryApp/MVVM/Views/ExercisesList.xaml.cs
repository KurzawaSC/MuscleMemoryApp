using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class ExercisesList : ContentPage
{
    string token;
	public ExercisesList(string _token)
	{
		InitializeComponent();
		BindingContext = new ExerciseViewModel(_token);
        token = _token;
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddExerciseView(token));
    }
}