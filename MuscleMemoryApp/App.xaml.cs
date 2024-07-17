using MuscleMemoryApp.MVVM.Views;

namespace MuscleMemoryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
