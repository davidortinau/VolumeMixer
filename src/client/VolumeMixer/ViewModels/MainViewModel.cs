using System;
using Xamarin.Forms;

namespace VolumeMixer.ViewModels
{
    public class MainViewModel
    {
        public Command ConnectCommand { get; set; }

        public MainViewModel()
        {
            ConnectCommand = new Command(OnConnect);
        }

        private void OnConnect()
        {
            Shell.Current.GoToAsync("settings");
        }
    }
}
