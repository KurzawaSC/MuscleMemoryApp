using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class LoginPage : ContentPage
{
    LoginPageViewModel _viewModel;
    public LoginPage()
	{
		InitializeComponent();
        _viewModel = new LoginPageViewModel();
        BindingContext = _viewModel;
    }

    private async void OnSomeEvent(object sender, EventArgs e)
    {
        await _viewModel.LogIn();
        _viewModel.eMail = string.Empty;
        _viewModel.password = string.Empty;
        await Navigation.PushAsync(new TabbedView());
    }

    private void Sign_In(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterView());
    }
}