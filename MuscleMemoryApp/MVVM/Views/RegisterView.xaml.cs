using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class RegisterView : ContentPage
{
	RegisterViewModel _viewModel;
	public RegisterView()
	{
		InitializeComponent();
		_viewModel = new RegisterViewModel();
		BindingContext = _viewModel;
	}

    private async void Confirm_Clicked(object sender, EventArgs e)
    {
		await _viewModel.RegisterUser();
		await Navigation.PopAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}