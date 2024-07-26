using MuscleMemoryApp.MVVM.ViewModels;

namespace MuscleMemoryApp.MVVM.Views;

public partial class EditUserDetailsView : ContentPage
{
    UserDetailsViewModel _viewModel;
    public EditUserDetailsView()
    {
        InitializeComponent();
        _viewModel = new UserDetailsViewModel();
        BindingContext = _viewModel;
    }

    private async void Confirm_Clicked(object sender, EventArgs e)
    {
        await _viewModel.EditUserInfo();
        await Navigation.PopAsync();
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