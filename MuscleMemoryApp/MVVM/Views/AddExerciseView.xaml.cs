using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class AddExerciseView : ContentPage
{
	public AddExerciseView(string _token)
	{
		InitializeComponent();
		BindingContext = new AddExerciseViewModel(_token);
	}
}