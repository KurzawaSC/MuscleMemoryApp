using MuscleMemoryApp.MVVM.Views;

namespace MuscleMemoryApp
{
    public partial class App : Application
    {
        public string? BearerToken { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            RetriveToken(MainPage);
        }

        private async void RetriveToken(Page mainPage)
        {
            BearerToken = await SecureStorage.Default.GetAsync("bearer_token");
            if (BearerToken != null)
            {
                await mainPage.Navigation.PushAsync(new TabbedView());
            }
        }
    }
}
