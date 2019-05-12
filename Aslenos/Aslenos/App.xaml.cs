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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTk4NzRAMzEzNzJlMzEyZTMwajJ1TmdoTTY1TFgzYXhGWnNnYmI4Y3NqL2EyNUZRZjcvZ0d0dmZQd1JLTT0=");
            InitializeComponent();

            DependencyService.Register<Bluetooth>();
            DependencyService.Register<Calculation>();
            DependencyService.Register<JsonDataKeeper<IList<Device>, Device>>();

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