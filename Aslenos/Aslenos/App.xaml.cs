using System.Collections.Generic;
using Aslenos.Services;
using Aslenos.Views;
using Xamarin.Forms;

using Device = Aslenos.Models.Device;

namespace Aslenos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Bluetooth>();
            DependencyService.Register<JsonDataKeeper<IList<Device>>>();
            DependencyService.Register<JsonDataKeeper<object>>();
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