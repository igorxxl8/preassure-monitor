#define DEBUG
using System;
using Aslenos.Helpers;
using Aslenos.Services;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PulsationAnalyzePage : ContentPage
    {
        private readonly DeviceDataViewModel _vm;
        private readonly StoppableTimer _timer;
        private readonly DeviceDataProvider _ddp;
        private readonly JsonDataKeeper<DeviceDataViewModel> _jsonKeeper;

        private Bluetooth Bluetooth { get; }

        private readonly MockImpulseInvoker _impulseInvoker;

        public PulsationAnalyzePage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();

#if DEBUG
            _impulseInvoker = new MockImpulseInvoker();
#endif

            _vm = new DeviceDataViewModel();
            _ddp = DeviceDataProvider.GetProvider;
            _timer = new StoppableTimer(TimeSpan.FromMilliseconds(Constants.UPDATE_INTERVAL), TimerTick);
            FirstChanelSeries.ItemsSource = _vm.FirstChanelSeriesData;
            SecondChanelSeries.ItemsSource = _vm.SecondChanelSeriesData;
            _jsonKeeper = new JsonDataKeeper<DeviceDataViewModel>();
        }

        private void TimerTick()
        {
            _vm.FirstChanelSeriesData.Add(_ddp.FirstChanel);

            _vm.SecondChanelSeriesData.Add(_ddp.SecondChanel);
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Stop();
#if DEBUG
            _impulseInvoker.Stop();
#else
            Bluetooth.SendCommand(Commands.STOP);
#endif
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Start();
#if DEBUG
            _impulseInvoker.Start();
#else
            Bluetooth.SendCommand(Commands.START);
#endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "preventLandScape");
        }

        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            _jsonKeeper.Filename = $"{DateTime.Now:MM_dd_yyyy_HH_mm_ss}.arc";
            _jsonKeeper.Save(_vm);
            DisplayAlert("Saved!", "", "OK");
        }
    }
}