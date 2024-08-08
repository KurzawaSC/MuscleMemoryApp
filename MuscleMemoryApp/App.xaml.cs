using MuscleMemoryApp.MVVM.Views;

namespace MuscleMemoryApp
{
    public partial class App : Application
    {
        public string? BearerToken { get; set; }
        public string baseUrl { get; set; }
        public App()
        {
            baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://IPV4:7002" : "https://localhost:7002";
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
