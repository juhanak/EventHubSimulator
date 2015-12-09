using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleSender
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SimpleAppServiceBus _bus = null;
        SendTimer _timer = new SendTimer(1000);
        int _lastValue = 0;
        int _lastSentValue = 0;

        public MainPage()
        {
            this.InitializeComponent();
            StartSendingAsync();
        }

        private async void StartSendingAsync()
        {
            if(await EventHubSettings.LoadSettingsFromFileAsync())
            {
                _bus = new SimpleAppServiceBus();
                _timer.PeriodExceeded += TimerExpired;
                _timer.ResetTimer();
            }
        }

        private async void TimerExpired(object sender, EventArgs e)
        {
            if(_lastValue != _lastSentValue)
            {
                await _bus.SendAsync(_lastValue);
                _lastSentValue = _lastValue;
            }
            _timer.ResetTimer();
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _lastValue = (int)e.NewValue;
        }
    }
}
