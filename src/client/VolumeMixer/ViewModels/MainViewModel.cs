using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VolumeMixer.ViewModels
{
    public class MainViewModel
    {
        public Command ConnectCommand { get; set; }
        public AsyncCommand<string> MuteCommand { get; set; }

        public MainViewModel()
        {
            ConnectCommand = new Command(OnConnect);
            MuteCommand = new AsyncCommand<string>(OnMute);
        }

        async Task OnMute(string channel)
        {
            // need a reference to the client so we can publish a message
            await (App.Current as App).Hermes.Publish($"mute/{channel}");
        }

        private void OnConnect()
        {
            Shell.Current.GoToAsync("settings");
        }
    }
}
