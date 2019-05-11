using Aslenos.Services;
using Aslenos.Views;
using Xamarin.Forms;

namespace Aslenos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Bluetooth>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}