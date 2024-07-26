using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class RegisterView : ContentPage
{
	UserDetailsViewModel _viewModel;
	public RegisterView()
	{
		InitializeComponent();
		_viewModel = new UserDetailsViewModel();
		BindingContext = _viewModel;
	}

    private async void Confirm_Clicked(object sender, EventArgs e)
    {
		if(_viewModel.request.password == _viewModel.repeatedPassword)
		{
            await _viewModel.RegisterUser();
            var login = new LoginPageViewModel()
            {
                eMail = _viewModel.request.email,
                password = _viewModel.request.password
            };
            await login.LogIn();
            await _viewModel.EditUserInfo();
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Passwords are diffrent", "Passwords must be equal", "Ok");
        }
		
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
		DatePicker datePicker = (DatePicker)sender;
		_viewModel.user.DateOfBirth = DateOnly.FromDateTime(datePicker.Date);
    }
}