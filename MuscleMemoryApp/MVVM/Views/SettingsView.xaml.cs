namespace MuscleMemoryApp.MVVM.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
	}

    private void LogOut_Clicked(object sender, EventArgs e)
    {
        var app = (App)Application.Current!;
        app.BearerToken = null;
        SecureStorage.Default.Remove("bearer_token");
        Navigation.PopToRootAsync();
    }

    private void UserDetails_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditUserDetailsView());
    }
}